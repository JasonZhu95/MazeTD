using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Enemy enemyTarget;
    private float damage;
    private float destroyDistance = 0.3f;

    public float speed = .2f;
    private bool initialized;

    public void Initialize(Enemy enemy, float damage)
    {
        enemyTarget = enemy;
        this.damage = damage;
        initialized = true;
    }

    private void Update()
    {
        if (initialized)
        {
            transform.position = Vector3.Lerp(transform.position, enemyTarget.transform.position, speed * Time.deltaTime);
            float distance = Vector3.Distance(transform.position, enemyTarget.transform.position);

            // Update arrow direction and rotation
            Vector3 direction = enemyTarget.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0f, 270f, 0f);
            transform.rotation = rotation;

            if (distance <= destroyDistance)
            {
                enemyTarget.Damage(damage);
                Destroy(gameObject);
            }
        }
    }
}
