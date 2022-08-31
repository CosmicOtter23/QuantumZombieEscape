using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HatShop : MonoBehaviour/*, IPointerEnterHandler, IPointerExitHandler*/
{
    public GameObject coin;
    private GameObject chains;
    public Text moneyText;

    public Button buyButton;
    public Text buyText;

    private string[] allHats = new string[] { "TopHat", "Fez", "Fedora", "Pineapple", "Bow", "Horn", "Eyemask", "Bunny Ears", "Poo", "Ice Cream", "Pirate", "Jester", "Riddler", "Crown", "Mario" };
    private string[] unlockedHats;
    private int[] hatCosts = new int[] { 15, 75, 20, 50, 5, 300, 10, 25, 100, 125, 30, 150, 40, 500, 200 };
    private bool[] locked = new bool[] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, };
    public GameObject[] chainsArray;

    private string hat;
    private int cost;

    private bool hatLocked = true;

    //public Button firstHat;

    private int index;

    void Start()
    {
        CreateHatList();

        hat = allHats[0];

        buyButton.gameObject.SetActive(false);

        Chains();

        for (int i = 0; i < locked.Count(); i++)
        {
            Debug.Log("locked " + i + ": " + locked[i]);
        }
    }

    void Update()
    {
        for (int i = 0; i < allHats.Count(); i++)
        {
            //Debug.Log(i);

            name = allHats[i];

            foreach (string name2 in unlockedHats)
            {
                if (name == name2)
                {
                    locked[i] = false;
                }
            }
        }

        moneyText.text = "Money: " + PlayerPrefs.GetInt("PrefMoney", 999);

        //Debug.Log(locked[index]);

        if (Input.GetKeyDown(KeyCode.H))
        {
            PlayerPrefs.DeleteKey("PrefHats");
        }

        Chains();
    }

    public void ButtonClick(string hatName)
    {
        hat = hatName;

        index = System.Array.IndexOf(allHats, hat);

        Debug.Log(index);

        cost = hatCosts[index];

        buyButton.gameObject.SetActive(true);

        if (locked[index])
        {
            buyText.text = "Buy";
        }
        else if (!locked[index])
        {
            buyText.text = "Equip";
        }

        for (int i = 0; i < locked.Count(); i++)
        {
            Debug.Log("locked " + i + ": " + locked[i]);
        }
    }

    public void BuyEquip()
    {
        if (locked[index])
        {
            if (PlayerPrefs.GetInt("PrefMoney") >= cost)
            {
                Debug.Log("Buy hat");
                PlayerPrefs.SetString("PrefHats", PlayerPrefs.GetString("PrefHats") + hat + "*");
                PlayerPrefs.SetInt("PrefMoney", PlayerPrefs.GetInt("PrefMoney") - cost);
                Debug.Log(PlayerPrefs.GetString("PrefHats"));
                locked[index] = false;
            }
            else
            {
                StartCoroutine(NotEnoughMoney());
            }
        }
        else if (!locked[index])
        {
            Debug.Log("Equip hat");
            PlayerPrefs.SetString("PrefCurrentHat", hat);
            Debug.Log(PlayerPrefs.GetString("PrefCurrentHat"));
        }

        Chains();
    }

    public void Chains()
    {
        for (int i = 0; i < allHats.Count(); i++)
        {
            if (locked[i])
            {
                //Debug.Log(i + ": locked");
                chainsArray[i].gameObject.SetActive(true);
            }
            else if (!locked[i])
            {
                //Debug.Log(i + ": unlocked");
                chainsArray[i].gameObject.SetActive(false);
            }
        }
    }

    public void CreateHatList()
    {
        unlockedHats = PlayerPrefs.GetString("PrefHats").Split(new char[] { '*' });
    }

    public IEnumerator NotEnoughMoney()
    {
        moneyText.color = Color.red;
        yield return new WaitForSeconds(1);
        moneyText.color = Color.black;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

//string PrefHats
//int PrefMoney
//string PrefCurrentHat
