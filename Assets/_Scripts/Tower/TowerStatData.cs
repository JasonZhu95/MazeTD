using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TowerStatData")]
public class TowerStatData : ScriptableObject
{
    public float damage;
    public float range;
    public float fireRate;
    public int cost;
    public int costToUpgrade;
}
