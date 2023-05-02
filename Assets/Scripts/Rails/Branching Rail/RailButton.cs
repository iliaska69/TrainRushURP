using UnityEngine;
using UnityEngine.Events;

public class RailButton : MonoBehaviour, IClickable
{
    [SerializeField] private UnityEvent isButtonClicked;


    public void Click()
    {
        isButtonClicked.Invoke();
    }
}
