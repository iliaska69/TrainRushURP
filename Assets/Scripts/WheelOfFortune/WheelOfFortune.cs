using System.Collections.Generic;
using UnityEngine;

public class WheelOfFortune<T>
{
    public T Roll(List<T> rewards)
    {
        var indexVictoryReward = Random.Range(0, rewards.Count);

        return rewards[indexVictoryReward];
    }
}
