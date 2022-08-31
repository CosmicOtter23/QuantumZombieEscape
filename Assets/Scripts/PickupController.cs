using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public float timeToNextPickup;

    public GameObject pickupPrefab;

    public List<Transform> spawnNodes;
    private Transform spawnNode;
    private int nodeNo;

    // Update is called once per frame
    void Update()
    {
        timeToNextPickup -= Time.deltaTime;
        
        nodeNo = Random.Range(0, spawnNodes.Count);
        spawnNode = spawnNodes[nodeNo];

        if (timeToNextPickup <= 0)
        {
            Instantiate(pickupPrefab, new Vector2(spawnNode.position.x, spawnNode.position.y), Quaternion.identity);
            timeToNextPickup = 20 + Random.Range(0, 10);
            //StartCoroutine(pickupPrefab.GetComponent<Pickup>().DestroyAfterTime(10, pickupPrefab));
        }
    }
}
