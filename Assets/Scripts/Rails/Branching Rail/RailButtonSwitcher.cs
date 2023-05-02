using UnityEngine;
using UnityEngine.Events;

public class RailButtonSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject rightButton;
    [SerializeField] private GameObject leftButton;
    [SerializeField] private UnityEvent isAllowRigthWay;
    [SerializeField] private UnityEvent isAllowLeftWay;


    public void RightButtonClicked()
    {
        isAllowRigthWay.Invoke();
    }

    public void LeftButtonClicked()
    {
        isAllowLeftWay.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        var train = other.gameObject.GetComponent<Trailer>();
        if (train is Trailer)
        {
            SetActiveButtons();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var train = other.gameObject.GetComponent<Trailer>();
        if (train is not null)
        {
            SetInactiveButtons();
        }
    }

    private void SetActiveButtons()
    {
        rightButton.SetActive(true);
        leftButton.SetActive(true);
    }

    private void SetInactiveButtons()
    {
        rightButton.SetActive(false);
        leftButton.SetActive(false);
    }
}
