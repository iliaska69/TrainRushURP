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


    public Reward GetNewWinningReward()
    {
        _winningReward = wheelOfFortune.Roll(wheelOfFortuneSO.rewards);

        return _winningReward;
    }

    [Button("GO")]
    public void OpenViewWheelOfFortune()
    {
        UIController.instance.OpenView(wheelOfFortuneSO.viewOfWheelOfFortune);
    }

    public List<Reward> GetListOfRewards()
    {
        return wheelOfFortuneSO.rewards;
    }

    public void ActivateReward()
    {
        _winningReward.Activate();
    }
}
