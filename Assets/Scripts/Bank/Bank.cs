using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bank : MonoBehaviour
{
    public static Bank instance = null;

    [SerializeField] private UnityEvent<int> balanceChangedEvent;

    private int balance = 0;
    private string BALANCE_PREFS = "Balance";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Load();
    }

    public bool CheckBalance(int price)
    {
        return true ? price <= balance : false;
    }

    public void AddMoney(int money)
    {
        balance += money;
        Save();
    }

    public void SubstractMoney(int money) 
    { 
        balance -= money;
        Save();
    }

    private void Save()
    {
        balanceChangedEvent.Invoke(balance);
        PlayerPrefs.SetInt(BALANCE_PREFS, balance);
    }

    private void Load()
    {
        balance = PlayerPrefs.GetInt(BALANCE_PREFS);
    }
}
