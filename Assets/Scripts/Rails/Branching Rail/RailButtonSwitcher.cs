using UnityEngine;
using UnityEngine.Events;

public class RailButtonSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject rightButton;
    [SerializeField] private GameObject leftButton;
    [SerializeField] private UnityEvent isAllowRigthWay;
    [SerializeField] private UnityEvent isAllowLeftWay;
    [SerializeField] private VoidEventChannelSO changeCameraFollowEvent;
    [SerializeField] private VoidEventChannelSO setToDefaultRotationCameraEvent;


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
        var trailer = other.gameObject.GetComponent<Trailer>();
        if (trailer is not Trailer) return;

        var mainTrailer = trailer.IsMainTrailer();
        if (!mainTrailer) return;

        SetActiveButtons();
        changeCameraFollowEvent.RaiseEvent();
    }

    private void OnTriggerExit(Collider other)
    {
        var trailer = other.gameObject.GetComponent<Trailer>();
        if (trailer is not Trailer) return;

        SetInactiveButtons();
        setToDefaultRotationCameraEvent.RaiseEvent();
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
