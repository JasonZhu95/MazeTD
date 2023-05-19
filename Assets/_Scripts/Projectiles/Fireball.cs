using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private float damage;

    public float speed = .2f;
    public float damageRadius = 1f;
    public Vector3 offset;

    public void Initialize(float damage)
    {
        this.damage = damage;
    }

    public void ApplyDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position + offset, damageRadius);
        for (int i = 0; i < hitEnemies.Length; i++)
        {
            Enemy enemy = hitEnemies[i].GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Damage(damage);
            }
        }
    }

    public void DestroyAnimationEvent()
    {
        Destroy(gameObject);
    }
}
