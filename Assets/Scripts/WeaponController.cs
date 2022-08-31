using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera cam;

    public GameObject player;

    Vector2 mousePos;

    private List<string> weaponList = new List<string> { };
    public List<string> allWeaponList = new List<string> { };
    public List<int> weaponBaseDamages;
    public List<Sprite> weaponSprites;
    public List<float> shotCooldowns;
    public List<int> bulletForces;
    public List<float> bulletSpread;
    public List<int> bulletCounts;
    public int noOfWeapons, weaponNo;

    private string currentWeapon, previousWeapon;

    public int damage, bulletCount;
    public float shotCooldown, bulletForce, spread;
    private float damageMultiplier = 1;

    public Sprite handgun, assaultRifle, magnum, minigun;

    public Transform firePoint;
    public GameObject bulletPrefab, grenadePrefab, rocketPrefab;

    private float shootTimer;

    public Text weaponText;
    private float weaponTextTime;

    private GameObject gameManager;

    public GameObject bullet;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");

        weaponNo = 0;
        currentWeapon = "Handgun";
        previousWeapon = "Handgun";
        damage = 40;
        shotCooldown = 0.5f;
        bulletForce = 20f;
        spread = 5f;
        bulletCount = 1;

        //Debug.Log("Weapons" + PlayerPrefs.GetInt("PrefWeapons"));

        for (int i = 0; i < 3 + PlayerPrefs.GetInt("PrefWeapons"); i++)
        {
            //Debug.Log("i: " + i);
            weaponList.Add(allWeaponList[i]);
        }

        noOfWeapons = weaponList.Count;
    }
    
    void Update()
    {
        weaponTextTime -= Time.deltaTime;

        if (player.transform.localScale.x == -1)
        {
            weaponText.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (player.transform.localScale.x == 1)
        {
            weaponText.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        rb.transform.position = player.transform.position + new Vector3(0f, -0.1f);

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        shootTimer += Time.deltaTime;

        if (Input.GetButton("Shoot") && shootTimer >= shotCooldown)
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    public void NewWeapon(bool sameWeapon)
    {
        if (!sameWeapon)
        {
            do
            {
                weaponNo = Random.Range(0, noOfWeapons);
                //weaponNo = 5;
                Debug.Log(weaponNo);
                currentWeapon = weaponList[weaponNo];
                gameManager.GetComponent<GameController>().DisplayPlayerText(currentWeapon);
            } while (currentWeapon == previousWeapon);
        }

        previousWeapon = currentWeapon;

        //weaponList.Remove(currentWeapon);
        //weaponList.Remove();

        Transform weaponSprite = gameObject.transform.Find("Weapon");

        weaponSprite.GetComponent<SpriteRenderer>().sprite = weaponSprites[weaponNo];
        damage = (int)(weaponBaseDamages[weaponNo] * damageMultiplier);
        shotCooldown = shotCooldowns[weaponNo];
        bulletForce = bulletForces[weaponNo];
        spread = bulletSpread[weaponNo];
        bulletCount = bulletCounts[weaponNo];
    }

    public void Shoot()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            shootTimer = 0;

            spread = bulletSpread[weaponNo] / 2;

            spread = Random.Range(0 - spread, 0 + spread);

            firePoint.Rotate(0, 0, spread);

            if (weaponNo < 4 || weaponNo == 6)
            {
                bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                //GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }
            else if (weaponNo == 4)
            {
                bullet = Instantiate(grenadePrefab, firePoint.position, firePoint.rotation);
            }
            else if (weaponNo == 5)
            {
                bullet = Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);
            }

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.rotation += 90;
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

            firePoint.Rotate(0, 0, -spread);

            //bullet.GetComponent<Bullet>().timeToExplode = 2;
        }
    }

    //public void EnemyShot(Collider2D target)
    //{
    //    target.GetComponent<EnemyAI2>().DealDamage(damage);
    //}

    public void MultiplyDamage(float multiplier)
    {
        //damageMultiplier *= multiplier;
        //damage = (int) (weaponBaseDamages[weaponNo] * damageMultiplier);

        Debug.Log("Damage Multiplier: " + damageMultiplier);
        damageMultiplier = multiplier;
        damage = (int)(weaponBaseDamages[weaponNo] * damageMultiplier);
        Debug.Log("Damage Multiplier: " + damageMultiplier);
    }

    //public void Handgun()
    //{
    //    GetComponent<SpriteRenderer>().sprite = handgun;
    //    damage = (int)(40 * damageMultiplier);
    //    shotCooldown = 0.5f;
    //    bulletForce = 20f;
    //}
    //public void AssaultRifle()
    //{
    //    GetComponent<SpriteRenderer>().sprite = assaultRifle;
    //    damage = (int)(20 * damageMultiplier);
    //    shotCooldown = 0.2f;
    //    bulletForce = 25f;
    //}
    //public void Magnum()
    //{
    //    GetComponent<SpriteRenderer>().sprite = magnum;
    //    damage = (int)(80 * damageMultiplier);
    //    shotCooldown = 0.75f;
    //    bulletForce = 30f;
    //}
    //public void Minigun()
    //{
    //    GetComponent<SpriteRenderer>().sprite = minigun;
    //    damage = (int)(7 * damageMultiplier);
    //    shotCooldown = 0.1f;
    //    bulletForce = 50f;
    //}
}
