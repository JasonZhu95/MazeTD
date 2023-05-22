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

    public void LoadScene(string sceneName)
    {
        anim.SetBool("start", true);
        StartCoroutine(LoadSceneAfterAnim(sceneName));
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

    private void OnSceneLoaded(AsyncOperation asyncOp)
    {
        anim.SetBool("start", false);
    }

    private void Update()
    {
        progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, target, 3 * Time.deltaTime);
    }
}
