using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    public TowerStatData towerStatData;
    private EnemySpawner enemySpawner;

    protected float timeSinceLastFire = 0f;
    private Vector3 transformOffset = new Vector3(.5f, .5f, 0);
    private GameObject finalEnemyDestination;

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        finalEnemyDestination = GameObject.FindWithTag("EnemyTarget");
    }

    protected Enemy FindTarget()
    {
        float shortestDistance = Mathf.Infinity;
        int closestEnemyIndex = 0;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position + transformOffset, towerStatData.range);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Enemy enemy = hitColliders[i].GetComponent<Enemy>();
            if (enemy != null)
            {
                float tempDistance = Vector3.Distance(finalEnemyDestination.transform.position, enemy.gameObject.transform.position);
                if (tempDistance < shortestDistance)
                {
                    shortestDistance = tempDistance;
                    closestEnemyIndex = i;
                }
            }
        }
        return hitColliders[closestEnemyIndex].GetComponent<Enemy>();

    }
}
