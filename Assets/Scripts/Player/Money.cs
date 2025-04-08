using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    // int currentMoney = 0;
    [SerializeField] DataContainer data;
    [SerializeField] TMPro.TextMeshProUGUI moneyText;

    public void Start()
    {
        SetMoneyText(data.coins);
    }

    public void AddMoney(int amount)
    {
        data.coins += amount;
        SetMoneyText(data.coins);
    }

    public void SetMoneyText(int money)
    {
        if (moneyText != null)
        {
            moneyText.text = "MONEY: " + data.coins.ToString();
        }
    }
}
