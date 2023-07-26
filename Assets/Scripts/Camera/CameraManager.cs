using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Component defaultCameraMode;
    [SerializeField] private Component zoomOutCameraMode;


    public void ActivateCameraMode(BaseCameraMode cameraMode)
    {
        var modes = GetComponent<BaseCameraMode>();
        Debug.Log(modes);
        cameraMode.gameObject.SetActive(true);
    }
}
