using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateController : MonoBehaviour
{
    public GameObject weapon;
    public GameObject pickupEffect;

    public int cratePoints;

    public GameObject gameManager;
    
    void Start()
    {
        weapon = GameObject.Find("Arm");
        gameManager = GameObject.Find("GameManager");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            weapon.GetComponent<WeaponController>().NewWeapon(false);
            Instantiate(pickupEffect, gameObject.transform.position, gameObject.transform.rotation);
            gameManager.GetComponent<ScoreManager>().AddPoints(cratePoints);
            Destroy(gameObject);
        }
    }
}
