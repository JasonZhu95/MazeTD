using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }

}
