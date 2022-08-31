using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSpawn : MonoBehaviour
{
    public GameObject crate;

    public List<Transform> nodes;
    private int nodeCount;

    public float waitTime;
    private float timeToNextSpawn;

    private int nodeNo;
    private float xPos, yPos;

    // Start is called before the first frame update
    void Start()
    {
        nodeCount = nodes.Count;

        timeToNextSpawn = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        timeToNextSpawn -= Time.deltaTime;

        if (timeToNextSpawn <= 0f)
        {
            timeToNextSpawn = waitTime;
            SpawnCrate();
        }
    }

    public void SpawnCrate()
    {
        nodeNo = Random.Range(0, nodeCount);

        xPos = Random.Range(-13f, 7f);
        yPos = 6;

        //xPos = nodes[nodeNo].position.x;
        //yPos = nodes[nodeNo].position.y;

        Instantiate(crate, new Vector2(xPos, yPos), Quaternion.identity);
    }
}
