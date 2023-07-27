using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private void Start()
    {
        ActivateDefaultCameraMode();
    }

    public void ActivateZoomOutCameraMode()
    {
        gameObject.AddComponent<ZoomOutCameraMode>();
    }

    public void ActivateDefaultCameraMode()
    {
        gameObject.AddComponent<DefaultCameraMode>();
    }
}
