using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UIElements;

//The automated A* Pathfinding algorithm
public class EnemyAI2 : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public Health healthBar;

    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public Transform enemyGFX;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    public GameObject gameManager;

    private int noOfEnemies;

    public Vector2 distanceToPlayer;

    public int killPoints;

    private bool playerInvulnerable;

    public GameObject coin;
    public int coinsOnDeath;
    
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        target = GameObject.Find("Player").transform;

        gameManager = GameObject.Find("GameManager");
        noOfEnemies = gameManager.GetComponent<SpawnEnemy>().enemyCount;

        InvokeRepeating("UpdatePath", 0f, .5f);

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    //public void DealDamage (int damage)
    //{
    //    TakeDamage(damage);
    //}

    void Update()
    {
        if (currentHealth <= 0)
        {
            EnemyDeath();
        }

        playerInvulnerable = gameManager.GetComponent<PlayerLives>().invulnerable;

        distanceToPlayer = target.position - transform.position;

        float floatDistanceToPlayer = Mathf.Sqrt(distanceToPlayer.x * distanceToPlayer.x + distanceToPlayer.y * distanceToPlayer.y);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    void OnPathComplete(Path p )
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    
    void FixedUpdate()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (force.x >= 0.01)
        {
            gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
            
        }
        else if (force.x <= -0.01)
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && playerInvulnerable == false)
        {
            gameManager.GetComponent<GameController>().PlayerDeath();
        }
    }

    public void EnemyDeath()
    {
        for (int i = 0; i < coinsOnDeath; i++)
        {
            Instantiate(coin, gameObject.transform.position, gameObject.transform.rotation);
        }
        
        gameManager.GetComponent<ScoreManager>().AddPoints(killPoints);
        Destroy(gameObject);
    }
}

