using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy")]
public class EnemySO : ScriptableObject
{
    public float moveSpeed;
    public float maxHealth;
}
