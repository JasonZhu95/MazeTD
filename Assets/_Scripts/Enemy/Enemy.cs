using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EnemySO enemyData;

    [SerializeField] private Image healthBarSprite;
    [SerializeField] private Animator enemyAnim;
    [SerializeField] private Collider2D enemyColl;

    public void Damage(float damage)
    {
        Debug.Log("Enemy damaged: " + damage);
        enemyData.currentHealth -= damage;

        if (enemyData.currentHealth <= 0)
        {
            enemyColl.enabled = false;
            enemyAnim.SetBool("dead", true);
        }
        else
        {
            UpdateHealthBar(enemyData.maxHealth, enemyData.currentHealth);
        }
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthBarSprite.fillAmount = currentHealth / maxHealth;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
