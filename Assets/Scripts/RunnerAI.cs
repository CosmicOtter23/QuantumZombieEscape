using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerAI : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public Health healthBar;

    public Transform target;

    public float speed;

    Rigidbody2D rb;

    public GameObject gameManager;

    private bool playerInvulnerable;

    public int killPoints;

    public GameObject coin;
    public int coinsOnDeath;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    private bool hittingWall;

    public bool moveRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        target = GameObject.Find("Player").transform;

        gameManager = GameObject.Find("GameManager");

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        //speed = Random.Range(2.5f, 3.5f);
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            EnemyDeath();
        }

        playerInvulnerable = gameManager.GetComponent<PlayerLives>().invulnerable;

        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        if (hittingWall)
            moveRight = !moveRight;

        if (moveRight)
        {
            transform.localScale = new Vector3(2f, 2f, 2f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
            transform.localScale = new Vector3(-2f, 2f, 2f);
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && playerInvulnerable == false)
        {
            gameManager.GetComponent<GameController>().PlayerDeath();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}
