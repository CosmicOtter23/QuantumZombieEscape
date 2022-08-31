using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DrBananas : MonoBehaviour
{
    private GameObject gameManager;

    public float moveSpeed;

    public Transform player, bananaGunL, bananaGunR;

    private Rigidbody2D rb;

    public Sprite still, left, right;

    public float shotCooldown;
    private float countdownR, countdownL, random;

    public int maxHealth;
    public int currentHealth;
    public Health healthBar;

    public bool dying = false;
    float blastCountdown;
    public ParticleSystem blastParticle;
    private SpriteRenderer rend;
    bool colourSwap = false;
    public float dyingDuration;
    private float currentDyingDuration;

    public int killPoints;
    public GameObject coin;
    public int coinsOnDeath;
    
    private bool onScreen;
    private Vector2 direction;
    private float distance;
    public float moveOntoScreenTime;

    private bool playerInvulnerable;

    public GameObject sprite;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        player = GameObject.Find("Player").transform;
        bananaGunL = GameObject.Find("Banana Gun L").transform;
        bananaGunR = GameObject.Find("Banana Gun R").transform;
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();

        if (player == null)
        {
            Debug.Log("player not found");
        }
        if (bananaGunL == null)
        {
            Debug.Log("banana gun L not found");
        }
        if (bananaGunR == null)
        {
            Debug.Log("banana gun R not found");
        }
        if (rend == null)
        {
            Debug.Log("renderer not found");
        }
        if (gameManager == null)
        {
            Debug.Log("game manager not found");
        }
        if (anim == null)
        {
            Debug.Log("animator not found");
        }

        rb = GetComponent<Rigidbody2D>();

        countdownL = shotCooldown;
        countdownR = shotCooldown;

        random = Random.Range(-4f, 4f);

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        blastCountdown = 0.2f;

        currentDyingDuration = dyingDuration;

        onScreen = false;

        GetComponent<BossFSM>().enabled = false;

        anim.SetBool("Dying", false);

        //direction = new Vector2(-0.27f, 0.31f) - new Vector2(transform.position.x, transform.position.y);
        //distance = direction.magnitude;
        //moveOntoScreenTime = 30;
    }

    // Update is called once per frame
    void Update()
    {
        playerInvulnerable = gameManager.GetComponent<PlayerLives>().invulnerable;

        //transform.Translate(direction * (Time.deltaTime * (distance / moveOntoScreenTime)));

        anim.SetFloat("Velocity", rb.velocity.x);

        if (!onScreen)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(0, 4.22f), 3 * Time.deltaTime);

            healthBar.SetHealth(currentHealth);

            if (transform.position.x >= 0)
            {
                onScreen = true;
                GetComponent<BossFSM>().enabled = true;
            }
        }
        else
        {
            //if (player.position.x - transform.position.x > -1 && player.position.x - transform.position.x < 1/* || transform.position.x < -7 || transform.position.x > 7*/)
            //{
            //    //gameObject.transform.position = new Vector2(-7, transform.position.y); 
            //    rb.velocity = new Vector2(0, 0);
            //    GetComponent<SpriteRenderer>().sprite = still;
            //}
            //else if (player.position.x < transform.position.x)
            //{
            //    rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            //    GetComponent<SpriteRenderer>().sprite = left;
            //}
            //else if (player.position.x > transform.position.x)
            //{
            //    rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            //    GetComponent<SpriteRenderer>().sprite = right;
            //}
            if (transform.position.x < -7)
            {
                transform.position = new Vector2(-7, transform.position.y);
            }
            if (transform.position.x > 7)
            {
                transform.position = new Vector2(7, transform.position.y);
            }

            if (rb.velocity.x < 0)
            {
                sprite.GetComponent<SpriteRenderer>().sprite = left;
            }
            else if (rb.velocity.x == 0)
            {
                sprite.GetComponent<SpriteRenderer>().sprite = still;
            }
            else if (rb.velocity.x > 0)
            {
                sprite.GetComponent<SpriteRenderer>().sprite = right;
            }

            countdownL -= Time.deltaTime;
            countdownR -= Time.deltaTime;

            if (countdownL <= 0 && !dying)
            {
                countdownL = shotCooldown + Random.Range(-3f, 2f);
                bananaGunL.GetComponent<BananaShooter>().Shoot();
            }
            if (countdownR <= 0 && !dying)
            {
                countdownR = shotCooldown + Random.Range(-3f, 2f);
                bananaGunR.GetComponent<BananaShooter>().Shoot();
            }

            if (currentHealth < 0)
            {
                currentHealth = 0;
                Death();
            }

            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
            }

            healthBar.SetHealth(currentHealth);

            if (dying)
            {
                //Debug.Log(anim.GetBool("Dying"));
                currentHealth = 0;
                blastCountdown -= Time.deltaTime;
                currentDyingDuration -= Time.deltaTime;
                rb.velocity = new Vector2(0, 0);
            }

            if (currentDyingDuration <= 0)
            {
                dying = false;
                Destroy(bananaGunL.gameObject);
                Destroy(bananaGunR.gameObject);
                Destroy(gameObject);
            }

            if (blastCountdown <= 0)
            {
                rend.color = Color.white;
                blastCountdown = 0.2f;
                Vector2 blastPosition = new Vector2(transform.position.x + Random.Range(-5, 5), transform.position.y + Random.Range(-2, 2));
                Instantiate(blastParticle, blastPosition, Quaternion.identity);
                colourSwap = !colourSwap;
                if (colourSwap)
                {
                    rend.color = Color.red;
                }
                //This equation allows the total number of coins to be given but in set intervals.
                for (int i = 0; i < coinsOnDeath / (dyingDuration / blastCountdown); i++)
                {
                    //Debug.Log(i);
                    Instantiate(coin, gameObject.transform.position, gameObject.transform.rotation);
                }
                gameManager.GetComponent<ScoreManager>().AddPoints((int)(killPoints / (dyingDuration / blastCountdown)));
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //healthBar.SetHealth(currentHealth);
    }

    private void Death()
    {
        //anim.SetTrigger("DyingTrig");
        anim.SetBool("Healing", false);
        anim.SetBool("Spawning", false);
        anim.SetBool("Lasers", false);
        anim.SetBool("Dying", true);
        dying = true;
        gameManager.GetComponent<SpawnEnemy>().boss = false;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        bananaGunL.GetComponent<BananaShooter>().enabled = false;
        bananaGunR.GetComponent<BananaShooter>().enabled = false;
    }

    IEnumerator DeathFlashing()
    {
        rend.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        rend.color = Color.white;
        yield return new WaitForSeconds(0.2f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && playerInvulnerable == false)
        {
            gameManager.GetComponent<GameController>().PlayerDeath();
        }
        else if (other.tag == "Platform")
        {
            //Instantiate(blastParticle, other.GetComponent<Rigidbody2D>().position, Quaternion.identity);
            other.GetComponent<TilemapRenderer>().enabled = false;
            Destroy(other);
        }
        else if (other.tag == "Scenery")
        {
            //Instantiate(blastParticle, other.GetComponent<Rigidbody2D>().position, Quaternion.identity);
            other.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(other);
        }
    }
}
