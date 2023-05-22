using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Button startWaveButton;
    [SerializeField] private Animator startWaveButtonAnim;
    [SerializeField] public WaveDataSO[] WaveData;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GameObject gameWonCanvas;
    [SerializeField] private GameObject addCoinCanvas;
    private TextMeshProUGUI addCoinText;

    public int waveIndex { get; private set; } = 0;

    public List<GameObject> listOfEnemies = new List<GameObject>();

    private void Start()
    {
        Enemy.OnEnemyDeath += HandleEnemyDestroyed;
        addCoinText = addCoinCanvas.GetComponent<TextMeshProUGUI>();
    }
    private void OnDestroy()
    {
        Enemy.OnEnemyDeath -= HandleEnemyDestroyed;
    }

    private IEnumerator SpawnEnemiesWithDelay()
    {
        for (int i = 0; i < WaveData[waveIndex].enemiesToSpawn.Length; i++)
        {
            for (int j = 0; j < WaveData[waveIndex].numberOfEnemies[i]; j++)
            {
                yield return new WaitForSeconds(.2f);
                float randomYOffset = Random.Range(-2.2f, 2.2f);
                float randomXOffset = Random.Range(-.7f, .7f);
                Vector3 spawnPosition = transform.position + new Vector3(randomXOffset, randomYOffset, 0);
                GameObject newObject = Instantiate(WaveData[waveIndex].enemiesToSpawn[i], spawnPosition, Quaternion.identity);
                listOfEnemies.Add(newObject);
            }
        }
    }

    public void StartSpawnOnClick()
    {
        StartCoroutine(SpawnEnemiesWithDelay());
        startWaveButton.interactable = false;
        startWaveButtonAnim.SetBool("start", true);
    }

    private void HandleEnemyDestroyed(Enemy enemy)
    {
        StartCoroutine(ClearListOfEnemies());
    }

    private IEnumerator ClearListOfEnemies()
    {
        yield return new WaitForSeconds(.1f);
        for (int i = listOfEnemies.Count - 1; i >= 0; i--)
        {
            if (listOfEnemies[i] == null)
            {
                listOfEnemies.RemoveAt(i);
            }
        }
        if(listOfEnemies.Count == 0)
        {
            float coinsToAdd = playerStats.CurrentCoins * .1f;
            coinsToAdd += 100;
            playerStats.AddCoins((int)Mathf.Ceil(coinsToAdd));
            addCoinText.text = Mathf.Ceil(coinsToAdd).ToString();
            addCoinCanvas.SetActive(true);
            waveIndex++;
            if (waveIndex == 31 && playerStats.CurrentHealth > 0)
            {
                gameWonCanvas.SetActive(true);
            }
            else
            {
                startWaveButton.interactable = true;
                startWaveButtonAnim.SetBool("start", false);
            }
        }
    }
}
