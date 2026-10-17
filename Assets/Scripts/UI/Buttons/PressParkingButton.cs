using UnityEngine;
using UnityEngine.EventSystems;

public class PressParkingButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private VoidEventChannelSO parkingEventSO;


    public void OnPointerDown(PointerEventData eventData)
    {
        parkingEventSO.RaiseEvent();
    }
}
