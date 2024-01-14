using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelOfFortuneUIController : MonoBehaviour
{
    [SerializeField] private List<Image> imagesOfRewards;
    [SerializeField] private WheelOfFortuneRollAnimation animationRollWheelOfFortune;

    private Reward _winingReward;
    private List<Reward> _rewards = new List<Reward>();


    private void Start()
    {
        Initialization();
    }

    public void Initialization()
    {
        _winingReward = CartWheelOfFortuneController.instance.GetNewWinningReward();
        var rewards = CartWheelOfFortuneController.instance.GetListOfRewards();
        
        for (var i = 0; i < rewards.Count; i++)
        {
            _rewards.Add(rewards[i]);
            imagesOfRewards[i].sprite = _rewards[i].Sprite;
        }
    }

    public void RollWheel()
    {
        int winneRewardIndex = 0;

        for (int i = 0; i < _rewards.Count; i++)
        {
            if(_winingReward == _rewards[i])
            {
                winneRewardIndex = i;
                break;
            }
        }

        if (winneRewardIndex >= 0)
            animationRollWheelOfFortune.Roll(winneRewardIndex, _rewards.Count);

        Debug.Log("Log");
    }
}
