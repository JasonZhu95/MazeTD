using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : BaseTower
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject fireballPrefab;

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
            if (timeSinceLastFire >= towerStatData.fireRate)
            {
                // Add FireTowerLogic
                anim.SetBool("attack", true);
                StartCoroutine(CreateFireball(enemy));
                timeSinceLastFire = 0f;
            }
        }
        timeSinceLastFire += Time.deltaTime;
    }

    public void FireAnimationEnd()
    {
        anim.SetBool("attack", false);
    }

    private IEnumerator CreateFireball(Enemy enemy)
    {
        yield return new WaitForSeconds(.2f);
        GameObject prefab = Instantiate(fireballPrefab, enemy.transform.position + offset, Quaternion.identity);
        prefab.GetComponent<Fireball>().Initialize(towerStatData.damage);

    }
}
