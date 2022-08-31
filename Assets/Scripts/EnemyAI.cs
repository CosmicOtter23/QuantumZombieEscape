using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public Health healthBar;

    public Transform enemyGFX;

    int currentWaypoint = 0;
    
    Rigidbody2D rb;

    public GameObject gameManager;

    private int noOfEnemies;

    public int killPoints;

    private bool playerInvulnerable;

    public GameObject coin;
    public int coinsOnDeath;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        gameManager = GameObject.Find("GameManager");
        noOfEnemies = gameManager.GetComponent<SpawnEnemy>().enemyCount;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            EnemyDeath();
        }

        playerInvulnerable = gameManager.GetComponent<PlayerLives>().invulnerable;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
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
