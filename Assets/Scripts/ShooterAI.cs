using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShooterAI : MonoBehaviour
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

    private float yDestination;

    private GameObject laser;
    public GameObject laserPrefab;
    public Transform firePoint;

    public float shootCooldown, countdown;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        target = GameObject.Find("Player").transform;

        gameManager = GameObject.Find("GameManager");

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        speed = Random.Range(2.5f, 3.5f);

        countdown = shootCooldown;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            EnemyDeath();
        }

        playerInvulnerable = gameManager.GetComponent<PlayerLives>().invulnerable;

        yDestination = target.position.y;

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, yDestination, 0), speed * Time.deltaTime);

        if (target.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        countdown -= Time.deltaTime;

        if (countdown <= 0)
        {
            countdown = shootCooldown;
            Shoot();
        }

        //////////
        //gameObject.GetComponent<PixelCollider2D>().Regenerate();
        //////////
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

    public void Shoot()
    {
        laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();
        rb.rotation += 90;
        rb.AddForce(firePoint.up * 10, ForceMode2D.Impulse);
    }
}
