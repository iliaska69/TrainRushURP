using UnityEngine;
using WheelOfFortune.Rewards;

public class CoinReward : Reward
{
    [SerializeField] private VoidEventChannelSO openWelcomeViewEventSO;
    
    public override void Activate()
    {
        Bank.instance.AddBalance(Count);
        
        Debug.Log($"Activate {Count} coins");
    }

    public override void InvokeActivateEvent()
    {
        openWelcomeViewEventSO.RaiseEvent();
    }
}
