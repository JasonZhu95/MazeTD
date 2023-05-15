using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    public TowerStatData towerStatData;

    private float timeSinceLastFire = 0f;
    private Vector3 transformOffset = new Vector3(.5f, .5f, 0);

    private Enemy firstEnemy;

    private void Update()
    {
        Fire();
    }

    private void FindFirstEnemy()
    {
        
    }

    private void Fire()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position + transformOffset, towerStatData.range);
        foreach (var hitCollider in hitColliders)
        {
            Enemy enemy = hitCollider.GetComponent<Enemy>();
            if (enemy != null && enemy == firstEnemy)
            {
                if (timeSinceLastFire >= towerStatData.fireRate)
                {
                    enemy.Damage(towerStatData.damage);
                    timeSinceLastFire = 0f;
                }
                break;
            }
        }
        timeSinceLastFire += Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + transformOffset, towerStatData.range);
    }
}
