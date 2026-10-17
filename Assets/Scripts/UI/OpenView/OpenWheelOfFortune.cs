using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class OpenWheelOfFortune : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO openWheelOfFortineEventSO;
    
    private TicketsManager _ticketsManager = new();
    
    private async void Start()
    {
        if(_ticketsManager.GetTickets() <= 0) return;
        
        await UniTask.Delay(TimeSpan.FromSeconds(0.7f));
        openWheelOfFortineEventSO.RaiseEvent();
    }
}
