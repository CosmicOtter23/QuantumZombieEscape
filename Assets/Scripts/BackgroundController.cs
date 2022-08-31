using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public GameObject scientistPrefab;
    private GameObject theScientist;
    private Rigidbody2D scientistRB;

    public float scientistMoveSpeed;

    public Transform[] spawnNodes = new Transform[] { };
    private Transform currentNode;

    private float countdown;
    private float maxTimeToSpawn = 20;
    
    void Start()
    {
        countdown = maxTimeToSpawn;
    }
    
    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0)
        {
            //Debug.Log("spawn scientist");

            maxTimeToSpawn *= 0.95f;
            countdown = Random.Range(0, maxTimeToSpawn);

            //Debug.Log("Time: " + countdown);

            currentNode = spawnNodes[Random.Range(0, 4)];

            theScientist = Instantiate(scientistPrefab, new Vector3(currentNode.transform.position.x, currentNode.transform.position.y, currentNode.transform.position.z), Quaternion.identity);

            scientistRB = theScientist.GetComponent<Rigidbody2D>();

            //if (theScientist.transform.position.x < 0)
            //{
            //    scientistRB.velocity = new Vector2(scientistMoveSpeed, scientistRB.velocity.y);
            //}
            //else
            //{

            //    transform.localScale = new Vector3(-1, 1, 1);
            //}


            //theScientist.transform.position = Vector3.MoveTowards(transform.position, new Vector3(-transform.position.x, transform.position.y, 0), scientistMoveSpeed * Time.deltaTime);
        }
    }
}
