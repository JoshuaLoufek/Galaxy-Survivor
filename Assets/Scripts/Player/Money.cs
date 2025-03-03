using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    int currentMoney = 0;

    [SerializeField] TMPro.TextMeshProUGUI moneyText;

    public void Start()
    {
        SetMoneyText(currentMoney);
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        SetMoneyText(currentMoney);
    }

    public void SetMoneyText(int money)
    {
        if (moneyText != null)
        {
            moneyText.text = "MONEY: " + money.ToString();
        }
    }
}
