using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyTrigger : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponent<Enemy>().enemyData.isBoss)
            {
                playerStats.TakeDamage(10);
            }
            else
            {
                playerStats.TakeDamage(1);
            }

            Destroy(collision.gameObject);
        }
    }

}
