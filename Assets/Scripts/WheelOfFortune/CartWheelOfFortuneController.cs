using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class CartWheelOfFortuneController : MonoBehaviour
{
    public static CartWheelOfFortuneController instance = null;

    [SerializeField] private WheelOfFortuneSO wheelOfFortuneSO;

    WheelOfFortune<Reward> wheelOfFortune = new();
    Reward _winningReward;


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

    public List<Reward> Rewards => wheelOfFortuneSO.rewards;

    public Reward GetNewWinningReward()
    {
        _winningReward = wheelOfFortune.Roll(wheelOfFortuneSO.rewards);

        return _winningReward;
    }

    public void OpenViewWheelOfFortune()
    {
        UIController.instance.OpenView(wheelOfFortuneSO.viewOfWheelOfFortune);
    }

    public void ActivateReward()
    {
        _winningReward.Activate();
    }
}
