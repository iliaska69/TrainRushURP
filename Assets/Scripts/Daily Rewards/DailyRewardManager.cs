using System;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardManager : MonoBehaviour
{
    public static DailyRewardManager instance;

    [SerializeField] private List<DailyRewardSO> _dailyRewards = new();

    private const string REWARD_INDEX = "Reward index";
    private const string LAST_DAY = "Last day";

    private int _rewardIndex;
    private int _lastDayIndex;

    private DateTime _lastRewardDateTime;

    private TicketsManager _ticketsManager = new();

    //private CoinsManager _coinsManager = new();
    private int _coins;

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
        //Delete this
        //PlayerPrefs.DeleteKey(REWARD_INDEX);
        //PlayerPrefs.DeleteKey(LAST_DAY);
    }

    public void ActivateReward()
    {
        if (!IsCanGetNextRewardAsync())
            return;

        _rewardIndex = PlayerPrefs.GetInt(REWARD_INDEX, 0);

        RememberInsidesOfTheReward();

        if (_dailyRewards.Count - 1 == _rewardIndex)
            _rewardIndex = 0;
        else
            _rewardIndex++;

        SaveDailyRewardPrefs();
    }

    public bool IsCanGetNextRewardAsync()
    {
        var networkTime = NetworkTimeKeeper.GetCurrentTime();
        if (!networkTime.HasValue)
        {
            Debug.LogWarning("Network time unavailable, denying reward");
            return false;
        }
        
        var lastDailyRewardTimeString = PlayerPrefs.GetString(LAST_DAY, "");
        if (string.IsNullOrEmpty(lastDailyRewardTimeString))
            return true;
   
        if (!DateTime.TryParse(lastDailyRewardTimeString, out _lastRewardDateTime))
        {
            Debug.LogWarning("Failed to parse last reward time, denying reward");
            return false;
        }
        
        return _lastRewardDateTime.Date < networkTime.Value.Date;
    }

    public List<DailyRewardSO> GetDailyRewards()
    {
        return _dailyRewards;
    }

    public int GetCurrentRewardIndex()
    {
        _rewardIndex = PlayerPrefs.GetInt(REWARD_INDEX, 0);
        
        return _rewardIndex;
    }

    public int GetCoinsToActivate()
    {
        var coins = Bank.instance.GetCoins();
        Bank.instance.RemoveCoins();
        
        return coins;
    }

    public int GetTicketsToActivate() => _ticketsManager.GetTickets();


    private void SaveDailyRewardPrefs()
    {
        PlayerPrefs.SetInt(REWARD_INDEX, _rewardIndex);
        PlayerPrefs.SetString(LAST_DAY, DateTime.Now.ToString());
        PlayerPrefs.Save();
    }

    private void RememberInsidesOfTheReward()
    {
        if (_dailyRewards[_rewardIndex].coins > 0)
            Bank.instance.AddBalance(_dailyRewards[_rewardIndex].coins);

        if (_dailyRewards[_rewardIndex].tickets > 0)
            _ticketsManager.AddTickets(_dailyRewards[_rewardIndex].tickets);
    }
}