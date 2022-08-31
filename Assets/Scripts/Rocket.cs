using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private GameObject weapon;

    //public int damage;

    public float explosionTime;

    public Sprite explodeSprite, rocketSprite;

    private Rigidbody2D rb;

    public GameObject explosionPrefab;

    public float timeToSpeedUp;
    private float countdown;

    public ParticleSystem explodeParticle;

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = rocketSprite;
        weapon = GameObject.Find("Arm");
        //damage = weapon.GetComponent<WeaponController>().damage;
        rb = gameObject.GetComponent<Rigidbody2D>();
        countdown = timeToSpeedUp;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector2 newForce = new Vector2(20, 0) * transform.forward;

        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            Debug.Log("Speed up");
            countdown = 10;
            //rb.rotation += 90;
            rb.AddForce(transform.right * 500);
            //rb.AddForce(rb.position + newForce);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ////Debug.Log("Collided with:" + other);

        //StartCoroutine(Explosion());

        Explosion2();
        Destroy(gameObject);

        //if (other.tag == "Enemy")
        //{
        //    other.GetComponent<EnemyAI2>().DealDamage(damage);
        //}
    }

    private IEnumerator Explosion()
    {
        Debug.Log("Explosion");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        gameObject.GetComponent<SpriteRenderer>().sprite = explodeSprite;
        gameObject.transform.localScale = new Vector3(2, 2, 2);
        yield return new WaitForSeconds(explosionTime);
        Destroy(gameObject);
    }

    public void Explosion2()
    {
        Debug.Log("Explosion2");
        Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Instantiate(explodeParticle, gameObject.transform.position, gameObject.transform.rotation);
    }
}
