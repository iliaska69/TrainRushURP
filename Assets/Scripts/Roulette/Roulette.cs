using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Roulette : MonoBehaviour
{
    [SerializeReference] List<IReward> rewards = new List<IReward>();
    [SerializeField] RouletteRollAnimation rollAnimation;

    int _indexVictoryReward;

    [Button("Roll")]
    void Roll()
    {
        _indexVictoryReward = Random.Range(0, rewards.Count);
        rollAnimation.Roll(_indexVictoryReward);
    }

    [Button("Add")]
    void Add()
    {
        rewards.Add(new CoinReward() { Coins = 100 });
        rewards.Add(new DimondReward() { dimonds = 100 });
    }
}
