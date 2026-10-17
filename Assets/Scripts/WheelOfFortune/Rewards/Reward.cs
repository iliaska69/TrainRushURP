using System;
using UnityEngine;

namespace WheelOfFortune.Rewards
{
    [Serializable]
    public abstract class Reward
    {
        public Sprite Sprite;
        public int Count;


        public abstract void Activate();
    
        public abstract void InvokeActivateEvent();
    }
}
