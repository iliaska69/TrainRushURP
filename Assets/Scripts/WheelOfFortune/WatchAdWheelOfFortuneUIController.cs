using UnityEngine;

public class WatchAdWheelOfFortuneUIController : MonoBehaviour
{
    [SerializeField] private UINavigator uiNavigator;
    [SerializeField] private VoidEventChannelSO ticketReceivedEvent;

    private readonly TicketsManager _ticketsManager = new();

    public void WatchAd()
    {
        //тут посмотреть рекламу
        Debug.LogWarning("[WatchAdWheelOfFortune] Reward ad is not assigned.");
        GiveTicket();
    }

    public void GiveTicket()
    {
        _ticketsManager.AddTickets(1);
        ticketReceivedEvent?.RaiseEvent();
        uiNavigator?.OpenView();
    }
}
