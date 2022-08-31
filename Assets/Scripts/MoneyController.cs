using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyController : MonoBehaviour
{
    public Text moneyStat;
    public int money;

    // Start is called before the first frame update
    void Start()
    {
        money = 0;
    }

    // Update is called once per frame
    void Update()
    {
        moneyStat.text = "Money: " + money;
    }

    public void AddMoney(int moneyToAdd)
    {
        money += moneyToAdd;
    }

    public void FinalMoney()
    {
        PlayerPrefs.SetInt("PrefMoney", PlayerPrefs.GetInt("PrefMoney") + money);
    }
}
