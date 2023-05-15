using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;
using System;

public class Enemy : MonoBehaviour
{
    public EnemySO enemyData;
    [SerializeField] private Image healthBarSprite;
    [SerializeField] private Animator enemyAnim;
    [SerializeField] private Collider2D enemyColl;

    private float currentHealth;

    // A* Pathfinding variables
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 200f;
    [SerializeField] private float nextWaypointDistance = 3f;

    private Path path;
    private int currentWaypoint = 0;
    private Seeker seeker;
    private Rigidbody2D rb;
    [SerializeField] private Canvas healthBarCanvas;

    public static event Action<Enemy> OnEnemyDeath;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("EnemyTarget").transform;
    }

    private void Start()
    {
        currentHealth = enemyData.maxHealth;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    private void Update()
    {
        if (path == null)
        {
            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (force.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            healthBarCanvas.GetComponent<RectTransform>().localScale = new Vector3(.001f, .001f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            healthBarCanvas.GetComponent<RectTransform>().localScale = new Vector3(-.001f, .001f, 1f);
        }
    }

    public void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }    
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public void Damage(float damage)
    {
        Debug.Log("Enemy damaged: " + damage);
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            enemyColl.enabled = false;
            enemyAnim.SetBool("dead", true);
        }
        else
        {
            UpdateHealthBar(enemyData.maxHealth, currentHealth);
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

    private void OnDestroy()
    {
        OnEnemyDeath?.Invoke(this);
    }
}
