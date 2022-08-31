using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private GameObject weapon;

    //public int damage;

    public float explosionTime;

    public Sprite explodeSprite, grenadeSprite;

    public float timeToExplode;

    private Rigidbody2D rb;

    public GameObject explosionPrefab;

    public ParticleSystem explodeParticle;

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = grenadeSprite;
        weapon = GameObject.Find("Arm");
        //damage = weapon.GetComponent<WeaponController>().damage;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timeToExplode -= Time.deltaTime;
        if (timeToExplode <= 0)
        {
            timeToExplode = 10;
            //StartCoroutine(Explosion());

            Explosion2();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //StartCoroutine(Explosion());

        if (other.tag == "Enemy" || other.tag == "Shooter" || other.tag == "Runner" || other.tag == "Boss")
        {
            Explosion2();
            Destroy(gameObject);
        }
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
