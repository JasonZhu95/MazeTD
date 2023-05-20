using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    [SerializeField] private List<GameObject> placedGameObjects = new List<GameObject>();
    [SerializeField] private GameObject towerUpgradeCanvas;
    private PlayerStats playerStats;
    private int towerCost;
    [SerializeField] private GameObject towerHolder;

    private void Awake()
    {
        playerStats = GameObject.FindWithTag("PlayerStat").GetComponent<PlayerStats>();
    }

    public int PlaceObject(GameObject prefab, Vector3 position)
    {
        FindObjectOfType<SoundManager>().Play("placeBuilding");
        towerCost = prefab.GetComponent<BaseTower>().towerStatData.cost;
        playerStats.DeductCoins(towerCost);
        GameObject newObject = Instantiate(prefab, towerHolder.transform);
        newObject.GetComponent<BaseTower>().TowerUpgradeCanvas = towerUpgradeCanvas;
        newObject.GetComponent<BaseTower>().DisableRangeIndicator();
        newObject.transform.position = position;
        newObject.layer = LayerMask.NameToLayer("Towers");
        placedGameObjects.Add(newObject);
        return placedGameObjects.Count - 1;
    }

    internal void RemoveObjectAt(int gameObjectIndex)
    {
        if (placedGameObjects.Count <= gameObjectIndex || placedGameObjects[gameObjectIndex] == null)
        {
            return;
        }

        FindObjectOfType<SoundManager>().Play("placeBuildingError");
        Destroy(placedGameObjects[gameObjectIndex]);
        placedGameObjects[gameObjectIndex] = null;
    }

    public void RefundTowerCost(GameObject prefab)
    {
        towerCost = prefab.GetComponent<BaseTower>().towerStatData.cost;
        playerStats.AddCoins(towerCost);
    }
}
