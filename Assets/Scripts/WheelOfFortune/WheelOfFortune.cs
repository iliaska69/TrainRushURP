using System.Collections.Generic;
using UnityEngine;

public class WheelOfFortune<T>
{
    public T Roll(List<T> rewards)
    {
        //var indexVictoryReward = Random.Range(1, 2); // this trailer reward
        //var indexVictoryReward = Random.Range(rewards.Count - 1, rewards.Count); // this ticket rewaard
        //var indexVictoryReward = Random.Range(3, 4); // big money
        
        var indexVictoryReward = Random.Range(0, rewards.Count);
        return rewards[indexVictoryReward];
    }
}