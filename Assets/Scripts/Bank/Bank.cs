using UnityEngine;
using UnityEngine.Events;

public class Bank : MonoBehaviour
{
    public static Bank instance = null;

    [SerializeField] private UnityEvent<int> balanceChangedEvent;
    [SerializeField] private VoidEventChannelSO shopAffordableChangedEvent;

    private int balance;
    private const string BALANCE_PREFS = "Balance";

    private int _coins;
    private const string COUNT_OF_LEVEL_COINS = "Count of new level coins";

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
        //возможно не будет работать, потому что старт будет позже чем в коинконтроллере
        Load();
        _coins = 0;
        
        SaveCoins();
        SaveBalance();
    }

    public bool CheckBalance(int price)
    {
        return price <= balance;
    }

    public void AddBalance(int money)
    {
        balance += money;
        _coins += money;

        SaveCoins();
        SaveBalance();
        NotifyBalanceChanged();
    }

    public void AddClearBalance(int money)
    {
        balance += money;
        NotifyBalanceChanged();
        SaveBalance();
    }
    
    public void SubstractBalance(int money)
    {
        balance -= money;
        NotifyBalanceChanged();
        SaveBalance();
    }

    private void NotifyBalanceChanged()
    {
        balanceChangedEvent.Invoke(balance);
        shopAffordableChangedEvent?.RaiseEvent();
    }

    public int GetBalance() => balance;

    public void AddCoins(int coins)
    {
        _coins += coins;
       
        SaveCoins();
    }
    
    public void RemoveCoins()
    {
        _coins = 0;
        SaveCoins();
    }

    public int GetCoins() => _coins;
    
    private void SaveBalance()
    {
        PlayerPrefs.SetInt(BALANCE_PREFS, balance);
    }
    
    private void SaveCoins()
    {
        PlayerPrefs.SetInt(COUNT_OF_LEVEL_COINS, _coins);
    }

    private void Load()
    {
        balance = PlayerPrefs.GetInt(BALANCE_PREFS);
        _coins = PlayerPrefs.GetInt(COUNT_OF_LEVEL_COINS);
    }
}