using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    private Rigidbody2D rb;
    public float spinSpeed;

    private GameObject gameManager;

    private bool playerInvulnerable;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        gameManager = GameObject.Find("GameManager");

        playerInvulnerable = gameManager.GetComponent<PlayerLives>().invulnerable;

        if (gameManager == null)
        {
            Debug.Log("GameManager not found");
        }
        if (rb == null)
        {
            Debug.Log("rigidbody not found");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rb.rotation = rb.rotation + spinSpeed;

        rb.angularVelocity = spinSpeed * transform.localScale.x;

        playerInvulnerable = gameManager.GetComponent<PlayerLives>().invulnerable;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !playerInvulnerable)
        {
            gameManager.GetComponent<GameController>().PlayerDeath();
            Destroy(gameObject);
        }
    }
}
