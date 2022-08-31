using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject gameManager;

    private bool playerInvulnerable;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        playerInvulnerable = gameManager.GetComponent<PlayerLives>().invulnerable;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !playerInvulnerable)
        {
            gameManager.GetComponent<GameController>().PlayerDeath();
        }
        Destroy(gameObject);
    }
}
