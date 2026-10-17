using UnityEngine;
using WheelOfFortune.Rewards;

public class TrailerReward : Reward
{
    [SerializeField] private VoidEventChannelSO openNewTrailerEventSO;
    
    public override void Activate()
    {
        Debug.Log($"Activate {Count} trailer");
    }

    public override void InvokeActivateEvent()
    {
        openNewTrailerEventSO.RaiseEvent();
    }
}
