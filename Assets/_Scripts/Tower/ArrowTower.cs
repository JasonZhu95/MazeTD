using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : BaseTower
{
    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        Enemy enemy = FindTarget();
        if (enemy != null)
        {
            Debug.Log(enemy.name);
            if (timeSinceLastFire >= towerStatData.fireRate)
            {
                enemy.Damage(towerStatData.damage);
                Debug.Log("Enemy damaged: " + towerStatData.damage);
                timeSinceLastFire = 0f;
            }
        }
        timeSinceLastFire += Time.deltaTime;
    }
}
