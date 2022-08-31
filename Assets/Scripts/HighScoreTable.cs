using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class HighScoreTable : MonoBehaviour
{
    //private List<int> scores = new List<int> { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
    //private List<string> names = new List<string> { "BOT", "BOT", "BOT", "BOT", "BOT", "BOT", "BOT", "BOT", "BOT", "BOT" };
    private List<int> scores = new List<int> { };
    private List<string> names = new List<string> { };
    public List<GameObject> entries;

    private int maxValue;
    private int index;
    private int listLength;

    private GameObject nextEntry;

    private Text nextScore;
    private Text nextName;

    //private int[] newScores;
    //private string[] newNames;

    //private int[,] templates = new int[2, 10] { { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 }, { "BOT", "BOT", "BOT", "BOT", "BOT", "BOT", "BOT", "BOT", "BOT", "BOT" } };

    void Start()
    {
        Time.timeScale = 1f;
    }

    private void Awake()
    {
        OrderScore();
        Leaderboard1();
    }

    public void Leaderboard1()
    {
        listLength = 10;

        for (int i = 0; i < 10; i++)
        {
            maxValue = Mathf.Max(scores.ToArray());
            Debug.Log("Index: " + index);
            index = scores.IndexOf(maxValue);

            Debug.Log(index);

            Debug.Log("high score: " + scores[index]);
            Debug.Log("name: " + names[index]);

            nextEntry = entries[i];

            nextScore = nextEntry.transform.Find("Score").GetComponent<Text>();
            nextName = nextEntry.transform.Find("Name").GetComponent<Text>();
            nextScore.text = scores[index].ToString();
            nextName.text = names[index].ToString();

            Debug.Log("List size: " + scores.Count());

            //RemoveAt(newScores, index);
            scores.RemoveAt(index);
            names.RemoveAt(index);

        }
    }

    public void OrderScore()
    {
        //newScores = PlayerPrefs.GetString("PrefScores").Split(new char[] { '*' })
        //    .Select(s =>
        //    {
        //        int i = 0;
        //        int.TryParse(s, out i);
        //        return i;
        //    })
        //    .ToArray();

        //newNames = PlayerPrefs.GetString("PrefScores").Split(new char[] { '*' }).ToArray();

        scores = PlayerPrefs.GetString("PrefScores").Split(new char[] { '*' })
            .Select(s =>
            {
                int i = 0;
                int.TryParse(s, out i);
                return i;
            })
            .ToList();

        names = PlayerPrefs.GetString("PrefNames").Split(new char[] { '*' }).ToList();

        Debug.Log(scores.Count());

        for (int i = 0; i < 10; i++)
        {
            Debug.Log(scores[i]);
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}


//PlayerPrefs.GetString("PrefScores")