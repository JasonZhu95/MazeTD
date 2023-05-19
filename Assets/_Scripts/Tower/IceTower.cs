using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : BaseTower
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject freezeAttack;

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
                StartCoroutine(SlowEnemies());
            }
        }
        timeSinceLastFire += Time.deltaTime;
    }

    private IEnumerator SlowEnemies()
    {
        yield return new WaitForSeconds(0f);
        freezeAttack.SetActive(true);
    }

    public void AnimationEventEnd()
    {
        anim.SetBool("attack", false);
    }
}
