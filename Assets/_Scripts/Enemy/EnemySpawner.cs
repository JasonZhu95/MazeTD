using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int numberOfEnemiesToSpawn;
    [SerializeField] private Button startWaveButton;
    [SerializeField] private Animator startWaveButtonAnim;
    [SerializeField] public WaveDataSO[] WaveData;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GameObject gameWonCanvas;

    public int waveIndex { get; private set; } = 0;

    public List<GameObject> listOfEnemies = new List<GameObject>();

    private void Start()
    {
        Enemy.OnEnemyDeath += HandleEnemyDestroyed;
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
                yield return new WaitForSeconds(1f);

                float randomYOffset = Random.Range(-1.5f, 3.5f);
                Vector3 spawnPosition = transform.position + new Vector3(0, randomYOffset, 0);
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
            waveIndex++;
            if (waveIndex == 11 && playerStats.CurrentHealth > 0)
            {
                gameWonCanvas.SetActive(true);
            }
            startWaveButton.interactable = true;
            startWaveButtonAnim.SetBool("start", false);
        }
    }
}
