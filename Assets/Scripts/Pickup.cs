using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Pickup : MonoBehaviour
{
    //public List<Sprite> pickupSprites;
    public List<string> pickupNames;

    public GameObject pickupEffect;
    public int pickupPoints;
    private Sprite pickupSprite;

    private int pickupNo = 0;

    private GameObject gameManager;
    private GameObject weapon;

    public Animator anim;

    private float pickupDuration, countdown;

    public GameObject canvas;
    public GameObject slider;

    public GameObject[] objects;
    public List<GameObject> objects2;
    private GameObject enemy;

    public ParticleSystem explosion;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        weapon = GameObject.Find("Arm");

        canvas = GameObject.Find("Canvas");
        slider = canvas.transform.Find("Pickup Duration").gameObject;

        pickupNo = Random.Range(0, pickupNames.Count);
        anim.SetInteger("PickupNo", pickupNo);

        pickupDuration = 10;
        countdown = 99999;
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            countdown = 0;
            Reset();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            Instantiate(pickupEffect, gameObject.transform.position, gameObject.transform.rotation);
            //StartCoroutine(PickupCall());
            PickupCall2();
            gameManager.GetComponent<ScoreManager>().AddPoints(pickupPoints);
        }
        //if (other.name == "Pickup")
        //{
        //    gameManager.GetComponent<PickupController>().timeToNextPickup = 0;
        //    Destroy(gameObject);
        //}
    }

    //IEnumerator PickupCall()
    //{
    //    //pickupNo = Random.Range(0, pickupNames.Count);
    //    //anim.SetInteger("PickupNo", pickupNo);

    //    gameManager.GetComponent<GameController>().DisplaySceneText(pickupNames[pickupNo]);

    //    if (pickupNames[pickupNo] == "Extra Life")
    //    {
    //        Debug.Log("Heart");
    //        gameManager.GetComponent<PlayerLives>().ExtraLife();

    //        GetComponent<SpriteRenderer>().enabled = false;
    //        GetComponent<BoxCollider2D>().enabled = false;
    //    }
    //    else if (pickupNames[pickupNo] == "X2 Coin")
    //    {
    //        Debug.Log("X2");

    //        gameManager.GetComponent<GameController>().DisplaySceneText("Double damage");
    //        weapon.GetComponent<WeaponController>().MultiplyDamage(2);

    //        pickupDuration = 5;

    //        GetComponent<SpriteRenderer>().enabled = false;
    //        GetComponent<BoxCollider2D>().enabled = false;

    //        slider.GetComponent<SceneSlider>().StartSlider(pickupDuration);
    //        slider.GetComponentInChildren<Image>().color = Color.magenta;

    //        yield return new WaitForSeconds(pickupDuration);

    //        slider.GetComponent<SceneSlider>().StopSlider();

    //        weapon.GetComponent<WeaponController>().MultiplyDamage(0.5f);

    //        Destroy(gameObject);
    //    }
    //    else if (pickupNames[pickupNo] == "X2 Score")
    //    {
    //        Debug.Log("X2 Score");

    //        gameManager.GetComponent<GameController>().DisplaySceneText("Double points");
    //        gameManager.GetComponent<ScoreManager>().scoreModifier *= 2;

    //        pickupDuration = 5;

    //        GetComponent<SpriteRenderer>().enabled = false;
    //        GetComponent<BoxCollider2D>().enabled = false;

    //        slider.GetComponent<SceneSlider>().StartSlider(pickupDuration);
    //        slider.GetComponentInChildren<Image>().color = Color.red;

    //        yield return new WaitForSeconds(pickupDuration);

    //        slider.GetComponent<SceneSlider>().StopSlider();

    //        gameManager.GetComponent<ScoreManager>().scoreModifier *= 0.5f;

    //        Destroy(gameObject);
    //    }
    //    else if (pickupNames[pickupNo] == "Nuke")
    //    {
    //        List<GameObject> enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
    //        List<GameObject> shooters = GameObject.FindGameObjectsWithTag("Shooter").ToList();
    //        List<GameObject> runners = GameObject.FindGameObjectsWithTag("Runner").ToList();

    //        foreach (GameObject i in enemies)
    //        {
    //            Instantiate(explosion, i.transform.position, Quaternion.identity);
    //            i.GetComponent<EnemyAI2>().EnemyDeath();
    //        }
    //        foreach (GameObject i in shooters)
    //        {
    //            Instantiate(explosion, i.transform.position, Quaternion.identity);
    //            i.GetComponent<ShooterAI>().EnemyDeath();
    //        }
    //        foreach (GameObject i in runners)
    //        {
    //            Instantiate(explosion, i.transform.position, Quaternion.identity);
    //            i.GetComponent<RunnerAI>().EnemyDeath();
    //        }

    //        Destroy(gameObject);
    //    }
    //}

    void Reset()
    {
        Debug.Log("Reset");
        slider.GetComponent<SceneSlider>().StopSlider();
        slider.SetActive(false);
        weapon.GetComponent<WeaponController>().MultiplyDamage(1f);
        gameManager.GetComponent<ScoreManager>().PointsModifier(1);
        Destroy(gameObject);
    }

    public void PickupCall2()
    {
        gameManager.GetComponent<GameController>().DisplaySceneText(pickupNames[pickupNo]);

        if (pickupNames[pickupNo] == "Extra Life")
        {
            Debug.Log("Heart");
            gameManager.GetComponent<PlayerLives>().ExtraLife();

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (pickupNames[pickupNo] == "X2 Coin")
        {
            Debug.Log("X2");

            slider.SetActive(true);

            gameManager.GetComponent<GameController>().DisplaySceneText("Double damage");
            weapon.GetComponent<WeaponController>().MultiplyDamage(2);

            pickupDuration = 10;

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            slider.GetComponent<SceneSlider>().StartSlider(pickupDuration);
            slider.GetComponentInChildren<Image>().color = Color.magenta;

            countdown = pickupDuration;
        }
        else if (pickupNames[pickupNo] == "X2 Score")
        {
            Debug.Log("X2 Score");

            slider.SetActive(true);

            gameManager.GetComponent<GameController>().DisplaySceneText("Double points");
            //gameManager.GetComponent<ScoreManager>().scoreModifier *= 2;
            gameManager.GetComponent<ScoreManager>().PointsModifier(2);

            pickupDuration = 10;

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            slider.GetComponent<SceneSlider>().StartSlider(pickupDuration);
            slider.GetComponentInChildren<Image>().color = Color.red;

            countdown = pickupDuration;
        }
        else if (pickupNames[pickupNo] == "Nuke")
        {
            List<GameObject> enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
            List<GameObject> shooters = GameObject.FindGameObjectsWithTag("Shooter").ToList();
            List<GameObject> runners = GameObject.FindGameObjectsWithTag("Runner").ToList();

            foreach (GameObject i in enemies)
            {
                Instantiate(explosion, i.transform.position, Quaternion.identity);
                i.GetComponent<EnemyAI>().EnemyDeath();
            }
            foreach (GameObject i in shooters)
            {
                Instantiate(explosion, i.transform.position, Quaternion.identity);
                i.GetComponent<ShooterAI>().EnemyDeath();
            }
            foreach (GameObject i in runners)
            {
                Instantiate(explosion, i.transform.position, Quaternion.identity);
                i.GetComponent<RunnerAI>().EnemyDeath();
            }

            Destroy(gameObject);
        }
    }
}
