using System;
using TMPro;
using UnityEngine;

public class UIMoney : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI money;

    private void Start()
    {
        var balance = PlayerPrefs.GetInt("Balance").ToString();
        var coins = PlayerPrefs.GetInt("Count of new level coins").ToString();
        var intBalance = int.Parse(balance);
        var intCoins = int.Parse(coins);
        intBalance -= intCoins;
        if (intBalance >= 1000)
        {
            var newIntBalance = (float)intBalance / 1000;
            var newFloatBalance = (float)Math.Round(newIntBalance, 2);
            money.text = newFloatBalance + "k";
        }
        else
        {
            money.text = intBalance.ToString();
        }
    }

    public void SetMoney(int value)
    {
        if (value >= 1000)
        {
            var newIntCoins = (float)value / 1000;
            var newFloatCoins = (float)Math.Round(newIntCoins, 2);
            money.text = newFloatCoins + "k";
        }
        else
        {
            money.text = value.ToString();
        }
    }
}
