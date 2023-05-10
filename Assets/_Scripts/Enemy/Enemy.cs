using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemySO enemyData;

    public void Damage(float damage)
    {
        enemyData.currentHealth -= damage;

        if (enemyData.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
