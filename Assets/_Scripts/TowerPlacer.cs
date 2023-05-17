using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    [SerializeField] private List<GameObject> placedGameObjects = new List<GameObject>();
    private PlayerStats playerStats;
    private int towerCost;

    private void Awake()
    {
        playerStats = GameObject.FindWithTag("PlayerStat").GetComponent<PlayerStats>();
    }

    public int PlaceObject(GameObject prefab, Vector3 position)
    {
        towerCost = prefab.GetComponent<BaseTower>().towerStatData.cost;
        playerStats.DeductCoins(towerCost);
        GameObject newObject = Instantiate(prefab);
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
        Destroy(placedGameObjects[gameObjectIndex]);
        placedGameObjects[gameObjectIndex] = null;
    }
}
