using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class NewWeaponUnlock : MonoBehaviour
{
    private int[] requiredScores = new int[] { 500, 1200, 2500, 5000 };
    private string[] weaponNames = new string[] { "Shotgun", "Grenade Launcher", "Rocket Launcher", "Minigun" };
    public Sprite[] gunSprites;

    private int finalScore;

    private int weaponsUnlocked;

    public GameObject weaponImage;
    public Text weaponNameText;

    public GameObject newWeaponBox;

    public Text pointsToNext;

    private int totalScore;

    // Start is called before the first frame update
    void Start()
    {
        totalScore = PlayerPrefs.GetInt("PrefTotalScore");
        weaponsUnlocked = PlayerPrefs.GetInt("PrefWeapons");
        newWeaponBox.SetActive(false);
    }

    public void UnlockWeapon()
    {
        newWeaponBox.SetActive(false);

        finalScore = gameObject.GetComponent<ScoreManager>().finalScore;

        totalScore = PlayerPrefs.GetInt("PrefTotalScore");

        //if (finalScore >= requiredScores[0] && weaponsUnlocked == 0)
        //{
        //    weaponImage.GetComponent<Image>().sprite = gunSprites[0];
        //    weaponNameText.text = weaponNames[0];
        //}

        for (int i = 0; i < weaponNames.Count(); i++)
        {
            if (totalScore >= requiredScores[i] && weaponsUnlocked == i)
            {
                newWeaponBox.SetActive(true);
                weaponImage.GetComponent<Image>().sprite = gunSprites[i];
                weaponNameText.text = weaponNames[i];
                pointsToNext.text = requiredScores[i + 1] - totalScore + " points to next weapon";
                PlayerPrefs.SetInt("PrefWeapons", PlayerPrefs.GetInt("PrefWeapons") + 1);
                break;
            }
        }
    }
}

//int PrefWeapons
//int PrefTotalScore