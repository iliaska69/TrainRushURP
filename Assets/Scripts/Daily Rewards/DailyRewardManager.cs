using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardManager : MonoBehaviour
{
    public static DailyRewardManager instance = null;

    [SerializeField] private List<DailyRewardSO> _dailyRewards = new List<DailyRewardSO>();

    private int _rewardIndex;
    private int _lastDayIndex;
    private const string REWARD_INDEX = "Reward index";
    private const string LAST_DAY_INDEX = "Last day index";
    private const string LAST_DAY = "Last day";
    private DateTime lastRewardDateTime = new DateTime();


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
        //PlayerPrefs.DeleteKey(REWARD_INDEX);
        //PlayerPrefs.DeleteKey(LAST_DAY_INDEX);
        //PlayerPrefs.DeleteKey(LAST_DAY);
    }

    [Button("Active")]
    public void ActivateReward()
    {
        if (!IsCanGetNextReward()) return;

        _rewardIndex = PlayerPrefs.GetInt(REWARD_INDEX, 0);
        var coins = _dailyRewards[_rewardIndex].coins;
        Bank.instance.AddMoney(coins);

        _rewardIndex++;
        PlayerPrefs.SetInt(REWARD_INDEX, _rewardIndex);
        PlayerPrefs.SetString(LAST_DAY, DateTime.Now.ToString());
        PlayerPrefs.SetInt(LAST_DAY_INDEX, 1);
    }

    public bool IsCanGetNextReward()
    {
        _lastDayIndex = PlayerPrefs.GetInt(LAST_DAY_INDEX, 0);
        if(_lastDayIndex == 0)
        {
            PlayerPrefs.SetString(LAST_DAY, DateTime.Now.ToString());
            
            return true;
        }

        var dateTimeNow = DateTime.Now;
        var lastDailyRawardTimeString = PlayerPrefs.GetString(LAST_DAY);
        lastRewardDateTime = DateTime.Parse(lastDailyRawardTimeString);
        var daysBetweenNowAndLast = dateTimeNow.Subtract(lastRewardDateTime).Days;
        if (daysBetweenNowAndLast >= 1)
            return true;

        return false;
    }

    public List<DailyRewardSO> GetDailyrewards()
    {
        return _dailyRewards;
    }
}
