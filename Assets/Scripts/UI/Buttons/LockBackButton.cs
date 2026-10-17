using UnityEngine;
using UnityEngine.UI;

public class LockBackButton : MonoBehaviour
{
    [SerializeField] private Button myButton;

    public void SetInteractable(bool value)
    {
        myButton.interactable = value;
    }
}
