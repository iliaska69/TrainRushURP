using System;
using UnityEngine;

[Serializable]
public abstract class Reward
{
    [SerializeField] private Sprite rewardSprite;
    [SerializeField] private int count;

    public abstract void Activate();

    public Sprite Sprite => rewardSprite;

    public int Count => count;
}
