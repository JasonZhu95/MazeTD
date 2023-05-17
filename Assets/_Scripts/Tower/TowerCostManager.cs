using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerCostManager : MonoBehaviour
{
    [SerializeField] private Button[] towerButtons;
    [SerializeField] private TowerStatData[] towerStats;
    [SerializeField] private PlayerStats playerStats;

    private void Start()
    {
        towerButtons = gameObject.GetComponentsInChildren<Button>();
    }

    private void Update()
    {
        for (int i = 0; i < towerButtons.Length; i++)
        {
            if (playerStats.CurrentCoins < towerStats[i].cost)
            {
                towerButtons[i].interactable = false;
            }
            else
            {
                towerButtons[i].interactable = true;
            }
        }
    }
}
