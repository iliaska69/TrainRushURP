using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public abstract class Reward
{
    public Sprite Sprite;
    public int Count;


    public abstract void Activate();
}
