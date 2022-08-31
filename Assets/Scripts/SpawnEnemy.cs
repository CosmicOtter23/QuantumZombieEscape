using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject theEnemy;
    public GameObject sworder;
    public GameObject shooter;
    public GameObject runner;
    public GameObject wallace;

    public int enemyCount = 0;
    public float enemiesToSpawn;

    public float waitTime;

    private float xPos = 0f;
    private float yPos = 0f;

    public List<Transform> spawnNodes;
    public List<Transform> shooterSpawnNodes;
    public List<Transform> runnerSpawnNodes;

    private int nodeCount;

    public int waves;
    public float timeBtwWaves;
    public float waveCountdown;
    public float waveTimeIncrease;

    private bool waveComplete = false;

    private int waveNo;
    public bool boss = false;

    void Start()
    {
        nodeCount = spawnNodes.Count;

        waveCountdown = timeBtwWaves;

        waveCountdown = 3;

        waveNo = 0;

        if (wallace == null)
        {
            Debug.Log("monkey not found");
        }
    }

    void Update()
    {
        if (!boss)
        {
            waveCountdown -= Time.deltaTime;
        }

        if (waveCountdown <= 0f)
        {
            waveNo += 1;

            if (waveNo == 12)
            {
                boss = true;
                Instantiate(wallace, new Vector2(0, 4.22f), Quaternion.identity);
            }
            else
            {
                waveCountdown = timeBtwWaves;
                enemyCount = 0;
                StartCoroutine(SpawnWave());
            }
        }
    }

    public IEnumerator SpawnWave()
    {
        timeBtwWaves += waveTimeIncrease;
        while (enemyCount < enemiesToSpawn)
        {
            enemyCount++;

            int rand = Random.Range(0, 20);

            //if (boss)
            //{
            //    //if (enemyCount % 4 == 1)
            //    //{
            //    //    //xPos = spawnNodes[5].position.x;
            //    //    //yPos = spawnNodes[5].position.y;
            //    //    theEnemy = sworder;
            //    //    Instantiate(theEnemy, new Vector2(-12, -5), Quaternion.identity);
            //    //}
            //    //else if (enemyCount % 4 == 2)
            //    //{
            //    //    //xPos = spawnNodes[6].position.x;
            //    //    //yPos = spawnNodes[6].position.y;
            //    //    theEnemy = sworder;
            //    //    Instantiate(theEnemy, new Vector2(12, -5), Quaternion.identity);
            //    //}

            //    //xPos = spawnNodes[6].position.x;
            //    //yPos = spawnNodes[6].position.y;
            //    //theEnemy = sworder;

            //    Debug.Log("Boss wave");
            //}
            if (rand >= 0 && rand < 10)
            {
                int nodeNo = Random.Range(0, nodeCount);

                xPos = spawnNodes[nodeNo].position.x;
                yPos = spawnNodes[nodeNo].position.y;

                theEnemy = sworder;
            }
            else if (rand >= 10 && rand < 18)
            {
                //if (rand < 14)
                //{
                //    xPos = Random.Range(-7, -8);
                //}
                //else if (rand >= 14)
                //{
                //    xPos = Random.Range(7, 8);
                //}

                int nodeNo = Random.Range(0, 2);

                xPos = shooterSpawnNodes[nodeNo].position.x;

                yPos = 6.5f;

                theEnemy = shooter;
            }
            else if (rand >= 18 && rand < 20)
            {
                //if (rand == 18)
                //{
                //    xPos = 12;
                //}
                //else if (rand >= 14)
                //{
                //    xPos = -12;
                //}

                int nodeNo = Random.Range(0, 2);

                xPos = runnerSpawnNodes[nodeNo].position.x;

                yPos = -5;

                theEnemy = runner;
            }

            Instantiate(theEnemy, new Vector2(xPos, yPos), Quaternion.identity);
            //Debug.Log("Spawn Enemy");
        
            yield return new WaitForSeconds(waitTime);
        }
        enemiesToSpawn = enemiesToSpawn + 0.4f;
    }
}
