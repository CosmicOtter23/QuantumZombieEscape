using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWarning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator EnemySpawning(float xPos, float yPos, float warningTime)
    {
        Instantiate(gameObject, new Vector2(xPos, yPos), Quaternion.identity);
        gameObject.SetActive(true);
        yield return new WaitForSeconds(warningTime);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
