using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLives : MonoBehaviour
{
    public GameObject player;
    private SpriteRenderer playerRend;

    public int playerMaxLives;
    private int playerLives;

    private Transform nextHeartPos;

    public Sprite emptyHeart, fullHeart;

    public List<Image> hearts;

    public bool invulnerable;
    public float invulnerableTime;

    public ParticleSystem deathParticle;
    
    void Start()
    {
        player = GameObject.Find("Player");
        playerRend = player.GetComponent<SpriteRenderer>();

        invulnerable = false;

        playerLives = playerMaxLives;

        foreach (Image image in hearts)
        {
            image.enabled = false;
        }

        //for (int i = 0; i < playerMaxLives; i++)
        //{
        //    //hearts[i].enabled = true;
        //    hearts[i].GetComponent<Image>().sprite = fullHeart;
        //    //hearts[i].sprite = fullHeart;
        //}

    }
    
    void Update()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < playerLives)
            {
                hearts[i].enabled = true;
            }
            if (i >= playerLives)
            {
                hearts[i].enabled = false;
            }
        }

        if (playerLives <= 0)
        {
            playerLives = 0;
            gameObject.GetComponent<GameController>().GameOver();
            gameObject.GetComponent<PlayerLives>().enabled = false;
        }

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    ExtraLife();
        //}
    }

    public void LoseLife()
    {
        playerLives -= 1;
        StartCoroutine(Invulnerability());
    }

    public void ExtraLife()
    {
        if (playerLives < hearts.Count)
        {
            playerLives += 1;
        }
        else
        {
            Debug.Log("Max Lives");
        }
    }

    IEnumerator Invulnerability()
    {
        invulnerable = true;
        Instantiate(deathParticle, player.transform.position, player.transform.rotation);
        //yield return new WaitForSeconds(invulnerableTime);

        playerRend.color = Color.red;
        yield return new WaitForSeconds(invulnerableTime / 6);
        playerRend.color = Color.white;
        yield return new WaitForSeconds(invulnerableTime / 6);
        playerRend.color = Color.red;
        yield return new WaitForSeconds(invulnerableTime / 6);
        playerRend.color = Color.white;
        yield return new WaitForSeconds(invulnerableTime / 6);
        playerRend.color = Color.red;
        yield return new WaitForSeconds(invulnerableTime / 6);
        playerRend.color = Color.white;
        yield return new WaitForSeconds(invulnerableTime / 6);

        //playerRend.enabled = false;
        //yield return new WaitForSeconds(invulnerableTime / 6);
        //playerRend.enabled = true;
        //yield return new WaitForSeconds(invulnerableTime / 6);
        //playerRend.enabled = false;
        //yield return new WaitForSeconds(invulnerableTime / 6);
        //playerRend.enabled = true;
        //yield return new WaitForSeconds(invulnerableTime / 6);
        //playerRend.enabled = false;
        //yield return new WaitForSeconds(invulnerableTime / 6);
        //playerRend.enabled = true;
        //yield return new WaitForSeconds(invulnerableTime / 6);

        invulnerable = false;
    }
}
