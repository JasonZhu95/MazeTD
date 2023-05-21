using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : BaseTower
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject freezeAttack;
    public Vector3 offset;

    private bool slowHasBeenCalled = false;

    private void Start()
    {
        freezeAttack.transform.localScale = new Vector3(towerStatData.range * (.3333f), towerStatData.range * (.3333f), 1f);
    }

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
                // FIRE Logic
                anim.SetBool("attack", true);
                freezeAttack.SetActive(true);
                if (!slowHasBeenCalled)
                {
                    StartCoroutine(SlowEnemies());
                    StartCoroutine(DamageEnemy());
                    slowHasBeenCalled = true;
                    timeSinceLastFire = 0f;
                }
            }
            else
            {
                slowHasBeenCalled = false;
            }
        }
        timeSinceLastFire += Time.deltaTime;
    }

    private IEnumerator SlowEnemies()
    {
        yield return new WaitForSeconds(.3f);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position + offset, towerStatData.range);
        for (int i = 0; i < hitEnemies.Length; i++)
        {
            Enemy enemy = hitEnemies[i].GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ApplySlow(.7f);
            }
        }
    }

    private IEnumerator DamageEnemy()
    {
        yield return new WaitForSeconds(.9f);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position + offset, towerStatData.range);
        for (int i = 0; i < hitEnemies.Length; i++)
        {
            Enemy enemy = hitEnemies[i].GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Damage(towerStatData.damage);
            }
        }
    }

    public void AnimationEventEnd()
    {
        anim.SetBool("attack", false);
    }
}
