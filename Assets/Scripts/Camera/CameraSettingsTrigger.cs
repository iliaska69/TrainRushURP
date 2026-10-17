using UnityEngine;

public class CameraSettingsTrigger : MonoBehaviour
{
    [SerializeField] private CameraSettingsEventChanelSO cameraSettingsEvent;
    [SerializeField] private Transform cameraPositionTransform;
    [SerializeField] private bool _isCameraPositionFixed;
    [SerializeField] private bool _isCameraRotationFixed;
    [SerializeField] private Vector3 cameraMoveDirection;

    private bool _isUsed;


    private void OnTriggerEnter(Collider other)
    {
        OnTrigger(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTrigger(other);
    }

    private void OnTrigger(Collider other)
    {
        if (_isUsed) return;
        var trailer = other.gameObject.GetComponent<Trailer>();
        if (trailer == null) return;
        if (!trailer.IsMainTrailer()) return;

        var cameraSettings = new CameraSettings(
            cameraPositionTransform,
            cameraPositionTransform.rotation,
            _isCameraPositionFixed,
            _isCameraRotationFixed,
            cameraMoveDirection
        );

        cameraSettingsEvent.RaiseEvent(cameraSettings);
        _isUsed = true;
    }
}