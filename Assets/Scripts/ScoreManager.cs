using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public static int score;
    public float scoreModifier;
    public int finalScore, highScore;

    public Text text, finalText;

    public GameObject canvas;
    public Text UserNameInputField;
    private string initials;

    private string currentNames, currentScores, newPref;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");

        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (score < 0)
            score = 0;

        text.text = "Score: " + score;

        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    AddPoints(10);
        //}
    }

    public void AddPoints(int pointsToAdd)
    {
        score += (int)(pointsToAdd * scoreModifier);
    }

    public void PointsModifier(float modifier)
    {
        scoreModifier = modifier;
    }

    public void NewScore()
    {
        initials = UserNameInputField.text;
        if (initials == "")
        {
            initials = "XXX";
        }

        currentNames = PlayerPrefs.GetString("PrefNames", "None");
        newPref = currentNames + initials + "*";
        PlayerPrefs.SetString("PrefNames", newPref);


        finalScore = score;
        currentScores = PlayerPrefs.GetString("PrefScores", "None");
        newPref = currentScores + finalScore.ToString() + "*";
        PlayerPrefs.SetString("PrefScores", newPref);
    }

    public void FinalScore()
    {
        finalScore = score;

        finalText.text = "Score: " + finalScore;

        PlayerPrefs.SetInt("PrefTotalScore", PlayerPrefs.GetInt("PrefTotalScore") + finalScore);

        Debug.Log("Total Score: " + PlayerPrefs.GetInt("PrefTotalScore"));
    }
}

//PlayerPrefs:
//string PrefNames
//string PrefScores