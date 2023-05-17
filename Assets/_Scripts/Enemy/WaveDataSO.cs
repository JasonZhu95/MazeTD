using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Wave Data")]
public class WaveDataSO : ScriptableObject
{
    public GameObject[] enemiesToSpawn;
}
