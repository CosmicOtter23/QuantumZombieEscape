using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistController : MonoBehaviour
{
    public float speed;
    private Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        destination = new Vector3(-transform.position.x, transform.position.y, 0);

        if (transform.position.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }
}
