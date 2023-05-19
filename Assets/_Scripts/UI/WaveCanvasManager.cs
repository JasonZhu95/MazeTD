using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveCanvasManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private Image[] enemyImages;
    [SerializeField] private TextMeshProUGUI[] enemyText;
    [SerializeField] private GameObject[] enemyPanelHolders;
    [SerializeField] private TextMeshProUGUI waveText;

    private WaveDataSO[] waveData;

    private void Awake()
    {
        waveData = enemySpawner.WaveData;
    }

    private void OnEnable()
    {
        waveText.text = (enemySpawner.waveIndex + 1).ToString();
        for (int i = 0; i < enemyPanelHolders.Length; i++)
        {
            enemyPanelHolders[i].SetActive(true);
        }
        for (int i = 0; i < waveData[enemySpawner.waveIndex].enemiesToSpawn.Length; i++)
        {
            SpriteRenderer sr = waveData[enemySpawner.waveIndex].enemiesToSpawn[i].GetComponent<SpriteRenderer>();
            Sprite sprite = sr.sprite;
            enemyImages[i].sprite = sprite;
            enemyText[i].text = waveData[enemySpawner.waveIndex].numberOfEnemies[i].ToString();
        }
        for (int i = waveData[enemySpawner.waveIndex].enemiesToSpawn.Length; i < enemyPanelHolders.Length; i++)
        {
            enemyPanelHolders[i].SetActive(false);
        }
    }
}
