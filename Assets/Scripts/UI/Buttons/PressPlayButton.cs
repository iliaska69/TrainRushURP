using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class PressPlayButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Button _button;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (_button.interactable)
        {
            _button.onClick.Invoke();
        }
    }
}
