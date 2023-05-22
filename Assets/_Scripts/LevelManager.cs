using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private GameObject loaderCanvas;
    [SerializeField] private Image progressBar;
    private Animator anim;
    private float target;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoadedWebGL;
    }

    public void LoadScene(string sceneName)
    {
        anim.SetBool("start", true);
#if UNITY_WEBGL
        StartCoroutine(LoadSceneAfterWebGL(sceneName));
#else
        StartCoroutine(LoadSceneAfterAnim(sceneName));
#endif
    }

    public IEnumerator LoadSceneAfterAnim(string sceneName)
    {
        yield return new WaitForSeconds(.5f);
        target = 0;
        progressBar.fillAmount = 0;

        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loaderCanvas.SetActive(true);
        do
        {
            target = scene.progress;
        } while (scene.progress < 0.9f);
        target = 1;


        scene.allowSceneActivation = true;
        loaderCanvas.SetActive(false);
        scene.completed += OnSceneLoaded;
    }

    public IEnumerator LoadSceneAfterWebGL(string sceneName)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoadedWebGL(Scene scene, LoadSceneMode mode)
    {
        anim.SetBool("start", false);
    }

    private void OnSceneLoaded(AsyncOperation asyncOp)
    {
        anim.SetBool("start", false);
    }

    private void Update()
    {
        progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, target, 3 * Time.deltaTime);
    }
}
