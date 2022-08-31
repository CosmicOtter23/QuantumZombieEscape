using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Update()
    {
        //Dev Commands

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("PlayerPrefs scores filled");
            PlayerPrefs.SetString("PrefScores", "5*20*50*100*150*200*500*750*1000*1500*");
            PlayerPrefs.SetString("PrefNames", "CPU*CPU*CPU*CPU*CPU*CPU*CPU*CPU*CPU*CPU*");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("PlayerPrefs scores cleared");
            PlayerPrefs.DeleteKey("PrefScores");
            PlayerPrefs.DeleteKey("PrefNames");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("All PlayerPrefs outputted");
            Debug.Log("Scores: " + PlayerPrefs.GetString("PrefScores"));
            Debug.Log("Names: " + PlayerPrefs.GetString("PrefNames"));
            Debug.Log("Money: " + PlayerPrefs.GetInt("PrefMoney"));
            Debug.Log("Total score: " + PlayerPrefs.GetInt("PrefTotalScore"));
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("PlayerPrefs money cleared");
            PlayerPrefs.SetInt("PrefMoney", 0);
            Debug.Log("Money: " + PlayerPrefs.GetInt("PrefMoney"));
        }
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    Debug.Log("New game");
        //    PlayerPrefs.SetString("PrefScores", "10*25*50*75*100*150*250*350*500*1000*");
        //    PlayerPrefs.SetString("PrefNames", "CPU*CPU*CPU*CPU*CPU*CPU*CPU*CPU*CPU*CPU*");
        //    PlayerPrefs.SetInt("PrefMoney", 0);
        //    PlayerPrefs.DeleteKey("PrefHats");
        //    PlayerPrefs.DeleteKey("PrefCurrentHat");
        //    PlayerPrefs.SetInt("PrefWeapons", 0);
        //}
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("Maxed out");
            PlayerPrefs.SetInt("PrefMoney", 9999);
            PlayerPrefs.SetInt("PrefWeapons", 4);
        }
    }

    public void PlayGame()
    {
        int rand = Random.Range(1, 4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + rand);
    }

    public void Leaderboard()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }

    public void HatShop()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 5);
    }

    public void NewGame()
    {
        Debug.Log("New game");
        PlayerPrefs.SetString("PrefScores", "5*20*50*100*150*200*500*750*1000*1500*");
        PlayerPrefs.SetString("PrefNames", "CPU*CPU*CPU*CPU*CPU*CPU*CPU*CPU*CPU*CPU*");
        PlayerPrefs.SetInt("PrefMoney", 0);
        PlayerPrefs.DeleteKey("PrefHats");
        PlayerPrefs.DeleteKey("PrefCurrentHat");
        PlayerPrefs.SetInt("PrefWeapons", 0);
        PlayerPrefs.SetInt("PrefTotalScore", 0);
        PlayGame();
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}



//PlayerPrefs:
//string PrefNames
//string PrefScores
//string PrefMoney
//string PrefHats