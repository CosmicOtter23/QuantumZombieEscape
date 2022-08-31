using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject weapon;
    public Transform firePoint;
    public GameObject bulletPrefab;

    private float shootTimer;
    public float shootCooldown;
    public float bulletForce;

    void Start()
    {
        weapon = GameObject.Find("Pistol");
        bulletForce = weapon.GetComponent<WeaponController>().bulletForce;
        shootCooldown = weapon.GetComponent<WeaponController>().shotCooldown;
        bulletForce = 20;
        shootCooldown = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer < shootCooldown)
        {
            return;
        }

        if (Input.GetButton("Shoot") && shootTimer >= shootCooldown)
        {
            Shoot();
        }
    }


    void Shoot()
    {
        shootTimer = 0;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.rotation += 90;
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
