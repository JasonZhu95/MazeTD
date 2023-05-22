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

    [SerializeField] private float currentHealth;
    private float currentSpeed;
    private SpriteRenderer SR;
    private Color storeColor;

    private bool isSlowed;
    private float slowedTimer;
    public float slowDuration = 2f;

    // A* Pathfinding variables
    [SerializeField] private Transform target;
    [SerializeField] private float nextWaypointDistance = 3f;

    private Path path;
    private int currentWaypoint = 0;
    private Seeker seeker;
    private Rigidbody2D rb;
    [SerializeField] private Canvas healthBarCanvas;
    private PlayerStats playerStats;

    public static event Action<Enemy> OnEnemyDeath;
    private bool dead = false;
    private Vector3 targetScale;
    private float lerpSpeed = 5f;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("EnemyTarget").transform;
        currentHealth = enemyData.maxHealth;
        currentSpeed = enemyData.moveSpeed;
        playerStats = GameObject.FindWithTag("PlayerStat").GetComponent<PlayerStats>();
        SR = gameObject.GetComponent<SpriteRenderer>();
        storeColor = SR.color;
    }

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
        isSlowed = false;
        slowedTimer = 0f;
    }

    private void Update()
    {
        if (path == null)
        {
            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * currentSpeed * Time.deltaTime;

        if (currentHealth > 0)
        {
            rb.AddForce(force);
        }

        if (isSlowed)
        {
            slowedTimer -= Time.deltaTime;

            if (slowedTimer <= 0f)
            {
                RemoveSlow();
            }
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        if (rb.velocity.x >= 0.01f)
        {
            targetScale = new Vector3(1f, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            targetScale = new Vector3(-1f, 1f, 1f);
        }
        Vector3 newScale = Vector3.Lerp(transform.localScale, targetScale, lerpSpeed * Time.deltaTime);
        transform.localScale = newScale;

        if (transform.localScale.x < 0f)
        {
            healthBarCanvas.GetComponent<RectTransform>().localScale = new Vector3(-.001f, .001f, 1f);
        }
        else
        {
            healthBarCanvas.GetComponent<RectTransform>().localScale = new Vector3(.001f, .001f, 1f);
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
        currentHealth -= damage;

        if (currentHealth <= 0 && !dead)
        {
            dead = true;
            enemyColl.enabled = false;
            enemyAnim.SetBool("dead", true);
            playerStats.AddCoins(enemyData.coinValue);
            rb.velocity = Vector3.zero;
        }
        UpdateHealthBar(enemyData.maxHealth, currentHealth);
    }

    public void ApplySlow(float slowMultiplier)
    {
        if (isSlowed)
        {
            slowedTimer += slowDuration;
        }
        else
        {
            float newSpeed = enemyData.moveSpeed * slowMultiplier;

            currentSpeed = newSpeed;
            Color targetColor = new Color(0.2f, 0.5f, 0.8f);
            SR.color = targetColor;

            isSlowed = true;
            slowedTimer = slowDuration;
            Invoke(nameof(RemoveSlow), slowDuration);
        }
    }

    private void RemoveSlow()
    {
        currentSpeed = enemyData.moveSpeed;
        SR.color = storeColor;
        isSlowed = false;
        slowedTimer = 0f;
    }


    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthBarSprite.fillAmount = currentHealth / maxHealth;
        if (currentHealth == 0)
        {
            healthBarSprite.fillAmount = 0;
        }
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
