using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : MonoBehaviour
{
    //public GameObject drBananas;
    Transform player;
    Rigidbody2D rb;
    float moveSpeed;

    public GameObject gameManager;

    public bool shooting;

    public GameObject theEnemy;

    public int healthToGain;

    public GameObject laser;
    public int lasersPerRound;
    public float timeBtwLasers;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start method");

        player = GetComponent<DrBananas>().player;
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = GetComponent<DrBananas>().moveSpeed;

        gameManager = GameObject.Find("GameManager");
        if (gameManager == null)
        {
            Debug.Log("Game manager not found");
        }

        shooting = false;

        GoToNextState();
    }

    enum BossState
    {
        Move,
        SpawnWave,
        Heal,
        Laser,
        COUNT
    }

    BossState currentState = BossState.Move;

    void GoToNextState()
    {
        bool loop = true;

        BossState nextState = currentState;
        string nextStateString = "", lastStateString = "";

        while (loop)
        {
            nextState = (BossState)Random.Range(0, (int)BossState.COUNT);

            nextStateString = nextState.ToString() + "State";

            lastStateString = currentState.ToString() + "State";

            if (nextStateString == lastStateString)
            {
                Debug.Log("Repeated state");
                loop = true;
            }
            else if (lastStateString != "MoveState" && nextStateString != "MoveState")
            {
                Debug.Log("2 non-move states");
                loop = true;
            }
            else
            {
                loop = false;
            }
        }

        currentState = nextState;

        StopCoroutine(lastStateString);
        StartCoroutine(nextStateString);
    }

    IEnumerator MoveState()
    {
        Debug.Log("Move State");

        float timeToMove = 10f;

        shooting = true;

        while (timeToMove > 0f)
        {
            timeToMove -= Time.deltaTime;

            if (player.position.x - transform.position.x > -1 && player.position.x - transform.position.x < 1/* || transform.position.x < -7 || transform.position.x > 7*/)
            {
                //gameObject.transform.position = new Vector2(-7, transform.position.y); 
                rb.velocity = new Vector2(0, 0);
            }
            else if (player.position.x < transform.position.x)
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }
            else if (player.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }

            yield return null;
        }

        shooting = false;

        rb.velocity = rb.velocity = new Vector2(0, 0);

        GoToNextState();
    }

    IEnumerator SpawnWaveState()
    {
        Debug.Log("Spawn Wave State");

        GetComponent<DrBananas>().anim.SetBool("Spawning", true);

        //StartCoroutine(gameManager.GetComponent<SpawnEnemy>().SpawnWave());
        //StartCoroutine(gameManager.GetComponent<SpawnEnemy>().SpawnWave());

        int enemiesToSpawn = 5;
        int enemyCount = 0;

        while (enemyCount < enemiesToSpawn)
        {
            if (enemyCount % 2 == 1)
            {
                Instantiate(theEnemy, new Vector2(-12, -5), Quaternion.identity);
            }
            else if (enemyCount % 2 == 0)
            {
                Instantiate(theEnemy, new Vector2(12, -5), Quaternion.identity);
            }

            enemyCount++;
            yield return new WaitForSeconds(0.5f);
        }

        GetComponent<DrBananas>().anim.SetBool("Spawning", false);

        yield return new WaitForSeconds(2);
        GoToNextState();
    }

    IEnumerator HealState()
    {
        Debug.Log("Healing State");

        GetComponent<DrBananas>().anim.SetBool("Healing", true);

        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(2);
            GetComponent<DrBananas>().currentHealth += healthToGain/3;
        }

        GetComponent<DrBananas>().anim.SetBool("Healing", false);

        GoToNextState();
    }

    IEnumerator LaserState()
    {
        Debug.Log("Laser State");

        GetComponent<DrBananas>().anim.SetBool("Lasers", true);

        for (int i = 0; i < lasersPerRound; i++)
        {
            Instantiate(laser, new Vector2(Random.Range(-11, 11), 0), Quaternion.identity);
            yield return new WaitForSeconds(timeBtwLasers);
        }

        GetComponent<DrBananas>().anim.SetBool("Lasers", false);

        GoToNextState();
    }
}
