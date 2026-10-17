using System;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Rewards;

public class CartWheelOfFortuneController : MonoBehaviour
{
    public static CartWheelOfFortuneController instance = null;

    [SerializeField] private WheelOfFortuneSO wheelOfFortuneSO;

    private WheelOfFortune<Reward> _wheelOfFortune = new();
    private Reward _winningReward;
    
    
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


    public Reward GetNewWinningReward()
    {
        _winningReward = _wheelOfFortune.Roll(wheelOfFortuneSO.rewards);
        
        ActivateReward();
        
        return _winningReward;
    }

    public void OpenViewWheelOfFortune()
    {
        UIController.instance.OpenView(wheelOfFortuneSO.viewOfWheelOfFortune);
    }

    public List<Reward> GetListOfRewards()
    {
        return wheelOfFortuneSO.rewards;
    }

    private void ActivateReward()
    {
        if (_winningReward is not TrailerReward)
        {
            _winningReward.Activate();
            return;
        }

        //проврека, если выпадет машинка и они все куплены, то вернется новый тикет
        var trailers = Shop.instance.GetUnpurchasedTrailers();
        if (trailers.Count == 0)
        {
            _winningReward = wheelOfFortuneSO.rewards[7];
        }

        _winningReward.Activate();
    }

    public void ActivateRewardEvent()
    {
        _winningReward.InvokeActivateEvent();
    }
}
