using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "CameraSettings/CameraSettings Event Channel")]
public class CameraSettingsEventChanelSO : SerializableBaseSO
{
    public UnityAction<CameraSettings> OnEventRaised;
	
    public void RaiseEvent(CameraSettings value)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(value);
        }
    }
}
