using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaShooter : MonoBehaviour
{
    private Rigidbody2D rb;
    
    private Transform drBananas, player;

    public float xOffset;

    public Transform firePoint;
    public GameObject banana, bananaPrefab;
    public float bananaForce;

    public bool shooting;

    void Start()
    {
        drBananas = GameObject.Find("Dr Wallace Bananas").transform;
        player = GameObject.Find("Player").transform;

        if (player == null)
        {
            Debug.Log("player not found");
        }
        if (drBananas == null)
        {
            Debug.Log("Monkey not found");
        }

        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        shooting = drBananas.GetComponent<BossFSM>().shooting;

        if (shooting)
        {
            Vector2 lookDir = new Vector2(player.position.x, player.position.y) - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            rb.rotation = angle + 90;
        }

        rb.transform.position = drBananas.transform.position + new Vector3(xOffset, -1f);
    }

    public void Shoot()
    {
        if (shooting)
        {
            Debug.Log("Shoot");

            banana = Instantiate(bananaPrefab, firePoint.position, firePoint.rotation);

            Rigidbody2D rb = banana.GetComponent<Rigidbody2D>();
            rb.rotation += 90;
            rb.AddForce(firePoint.up * -bananaForce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.Log("Not shooting");
        }
    }
}
