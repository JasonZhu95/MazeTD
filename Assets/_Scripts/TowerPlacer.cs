using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    [SerializeField] private List<GameObject> placedGameObjects = new List<GameObject>();

    public int PlaceObject(GameObject prefab, Vector3 position)
    {
        GameObject newObject = Instantiate(prefab);
        newObject.transform.position = position;
        placedGameObjects.Add(newObject);
        return placedGameObjects.Count - 1;
    }
}
