using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float coinSpeed;

    private Rigidbody2D rb;

    public ParticleSystem pickupEffect;

    public GameObject gameManager;

    public int coinValue;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager");

        rb.rotation += Random.Range(0, 360);

        Vector2 coinDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        Vector2 coinForce = coinDirection * coinSpeed * Time.deltaTime;
        rb.AddForce(coinForce, ForceMode2D.Impulse);
    }
    
    void Update()
    {
        
    }

    //public void NewCoin()
    //{
    //    rb = gameObject.GetComponent<Rigidbody2D>();

    //    Vector2 coinDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    //    //Vector2 coinDirection = gameObject.transform.rotation;       
    //    Vector2 coinForce = coinDirection * coinSpeed * Time.deltaTime;
    //    Debug.Log(coinForce);
    //    rb.AddForce(coinForce, ForceMode2D.Impulse);
    //}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            gameManager.GetComponent<MoneyController>().AddMoney(coinValue);
            Instantiate(pickupEffect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
