using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTower : BaseTower
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject lightningPrefab;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 lightningOffset;

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        Enemy enemy = FindTarget();
        if (enemy != null)
        {
            if (timeSinceLastFire >= towerStatData.fireRate)
            {
                // Add FireTowerLogic
                anim.SetBool("attack", true);
                GetAllEnemiesInRange();
                timeSinceLastFire = 0f;
            }
        }
        timeSinceLastFire += Time.deltaTime;
    }

    private void GetAllEnemiesInRange()
    {
        List<Enemy> enemiesInRange = new List<Enemy>();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position + offset, towerStatData.range);
        for (int i = 0; i < hitEnemies.Length; i++)
        {
            Enemy enemy = hitEnemies[i].GetComponent<Enemy>();
            if (enemy != null)
            {
                enemiesInRange.Add(enemy);
            }
        }
        StartCoroutine(CreateLightning(enemiesInRange));
    }

    private IEnumerator CreateLightning(List<Enemy> enemies)
    {
        yield return new WaitForSeconds(.2f);

        foreach (var enemy in enemies)
        {
            GameObject prefab = Instantiate(lightningPrefab, enemy.transform.position + lightningOffset, Quaternion.identity);
        }
        yield return new WaitForSeconds(.3f);
        
        foreach (var enemy in enemies)
        {
            enemy.Damage(towerStatData.damage);
        }
    }

    public void LightningAnimationEnd()
    {
        anim.SetBool("attack", false);
    }

}
