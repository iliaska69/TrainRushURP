using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CameraSettingsEvent : UnityEvent<CameraSettings>
{

}

public class CameraSettingsEventListener : MonoBehaviour
{
    [SerializeField] private CameraSettingsEventChanelSO _channel;

    public CameraSettingsEvent OnEventRaised;

    private void OnEnable()
    {
        if (_channel != null)
            _channel.OnEventRaised += Respond;
    }

    private void OnDisable()
    {
        if (_channel != null)
            _channel.OnEventRaised -= Respond;
    }

    private void Respond(CameraSettings value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}
