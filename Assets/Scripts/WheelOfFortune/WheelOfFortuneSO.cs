using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Rewards;

[CreateAssetMenu(fileName = "WheelOfFortune", menuName = "ScriptableObjects/WheelOfFortune", order = 4)]
public class WheelOfFortuneSO : ScriptableObject
{
    public View viewOfWheelOfFortune;

    [SerializeReference] public List<Reward> rewards = new List<Reward>();

    [BoxGroup("ChooseType")]
    [SerializeField] private TypeOfReward typeOfReward;


    [Button("Add")]
    void Add()
    {
        switch (typeOfReward)
        {
            case TypeOfReward.CoinReward:
                rewards.Add(new CoinReward());
                break;
            case TypeOfReward.TicketReward:
                rewards.Add(new TicketReward());
                break;
            case TypeOfReward.TrailerReward:
                rewards.Add(new TrailerReward());
                break;
        }
    }
}
