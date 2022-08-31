using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    public GameObject hitEffect;

    private GameObject weapon;

    private int weaponNo;
    public float timeToExplode;

    public GameObject explosion;

    void Start()
    {
        weapon = GameObject.Find("Arm");
        damage = weapon.GetComponent<WeaponController>().damage;
        //timeToExplode = 2;
    }

    void Update()
    {
        //timeToExplode -= Time.deltaTime;
        ////Debug.Log(timeToExplode);
        
        //if (weaponNo == 5 && timeToExplode <= 0)
        //{
        //    Explosion();
        //}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyAI>().TakeDamage(damage);
        }
        else if (other.tag == "Shooter")
        {
            other.GetComponent<ShooterAI>().TakeDamage(damage);
        }
        else if (other.tag == "Runner")
        {
            other.GetComponent<RunnerAI>().TakeDamage(damage);
        }
        else if (other.tag == "Boss")
        {
            Debug.Log("Hit Wallace");
            other.GetComponent<DrBananas>().TakeDamage(damage);
        }

        if (other.tag != "Character")
        {
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    //public void Explosion()
    //{
    //    Debug.Log("Explosion");

    //    Instantiate(explosion, transform.position, transform.rotation);
    //}
}
