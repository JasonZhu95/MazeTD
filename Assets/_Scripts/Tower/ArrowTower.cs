using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : BaseTower
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject archerGO;
    [SerializeField] private Animator anim;

    [SerializeField] private Vector3 offset;

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        Enemy enemy = FindTarget();
        if (enemy != null)
        {
            if (transform.position.x - enemy.transform.position.x < 0)
            {
                Vector3 newScale = archerGO.transform.localScale;
                newScale.x = 1f;
                archerGO.transform.localScale = newScale;
            }
            else
            {
                Vector3 newScale = archerGO.transform.localScale;
                newScale.x = -1f;
                archerGO.transform.localScale = newScale;
            }
            if (timeSinceLastFire >= towerStatData.fireRate)
            {
                anim.SetBool("attack", true);
                StartCoroutine(CreateArrow(enemy));
                timeSinceLastFire = 0f;
            }
        }
        timeSinceLastFire += Time.deltaTime;
    }
    public void ArcherAnimationEnd()
    {
        anim.SetBool("attack", false);
    }

    private IEnumerator CreateArrow(Enemy enemy)
    {
        yield return new WaitForSeconds(.2f);
        GameObject prefab = Instantiate(projectilePrefab, transform.position + offset, Quaternion.identity);
        prefab.GetComponent<Projectile>().Initialize(enemy, towerStatData.damage);
    }
}
