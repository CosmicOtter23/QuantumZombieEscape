using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public float bulletSpeed;

    private Rigidbody2D theRB;

    public GameObject projectileEffect;

    public int damage;

	// Use this for initialization
	void Start () {
        theRB = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update() {
        theRB.velocity = new Vector2(bulletSpeed * transform.localScale.x, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            //FindObjectOfType<GameManager>().HurtEnemy(damage);
		}
		Destroy(gameObject);
		Instantiate (projectileEffect, transform.position, transform.rotation);
    }
}
