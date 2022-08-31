using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject gameOverScreen;

    private float sceneTextCountdown = 0, playerTextCountdown = 0;

    public Text playerText, sceneText;

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        sceneTextCountdown -= Time.deltaTime;
        playerTextCountdown -= Time.deltaTime;

        if (playerTextCountdown <= 0)
        {
            playerText.text = "";
        }
        if (sceneTextCountdown <= 0)
        {
            sceneText.text = "";
        }
    }

    public void PlayerDeath()
    {
        Debug.Log("Player Death");
        gameObject.GetComponent<PlayerLives>().LoseLife();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
        gameObject.GetComponent<ScoreManager>().FinalScore();
        //PlayerPrefs.SetInt("PrefMoney", PlayerPrefs.GetInt("Money") + gameObject.GetComponent<MoneyController>().money);
        gameObject.GetComponent<MoneyController>().FinalMoney();
        gameObject.GetComponent<NewWeaponUnlock>().UnlockWeapon();
    }

    public static IEnumerator Waiter(int waitSeconds)
    {
        yield return new WaitForSecondsRealtime(waitSeconds);
    }

    public void DisplayPlayerText(string displayText)
    {
        playerText.text = displayText;
        playerTextCountdown = 1;
    }

    public void DisplaySceneText(string displayText)
    {
        sceneText.text = displayText;
        sceneTextCountdown = 1;
    }
}


//PlayerPrefs:
//string PrefNames
//string PrefScores
//string PrefMoney
//string PrefHats