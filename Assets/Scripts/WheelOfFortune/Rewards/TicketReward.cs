using UnityEngine;
using WheelOfFortune.Rewards;

public class TicketReward : Reward
{
    [SerializeField] private VoidEventChannelSO activateGettingTicketAnim;
    
    public override void Activate()
    {
        var ticketManager = new TicketsManager();
        ticketManager.AddTickets(Count);
        
        Debug.Log($"Activate {Count} ticket");
    }

    public override void InvokeActivateEvent()
    {
        activateGettingTicketAnim.RaiseEvent();
    }
}
