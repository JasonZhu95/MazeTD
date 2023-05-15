using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int numberOfEnemiesToSpawn;
    [SerializeField] private float spawnInterval = 1.0f;
    [SerializeField] private Button startWaveButton;
    [SerializeField] private Animator startWaveButtonAnim;

    private float timeSinceLastSpawn = 0.0f;
    private bool startSpawning = false;
    private int enemiesSpawned = 0;

    public List<GameObject> listOfEnemies = new List<GameObject>();

    private void Start()
    {
        Enemy.OnEnemyDeath += HandleEnemyDestroyed;
    }
    private void OnDestroy()
    {
        Enemy.OnEnemyDeath -= HandleEnemyDestroyed;
    }

    private void Update()
    {
        if (startSpawning)
        {
            if (enemiesSpawned < numberOfEnemiesToSpawn)
            {
                timeSinceLastSpawn += Time.deltaTime;

                if (timeSinceLastSpawn >= spawnInterval)
                {
                    SpawnEnemy();
                    timeSinceLastSpawn = 0.0f;
                }
            }
        }
    }

    public void StartSpawnOnClick()
    {
        startSpawning = true;
        startWaveButton.interactable = false;
        startWaveButtonAnim.SetBool("start", true);
    }

    private void SpawnEnemy()
    {
        float randomYOffset = Random.Range(-1.5f, 3.5f);
        Vector3 spawnPosition = transform.position + new Vector3(0, randomYOffset, 0);
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        listOfEnemies.Add(newEnemy);
        enemiesSpawned++;
    }

    private void HandleEnemyDestroyed(Enemy enemy)
    {
        Debug.Log("Enemy Destroyed");
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
            startWaveButtonAnim.SetBool("start", false);
            startSpawning = false;
            enemiesSpawned = 0;
        }
    }

}
