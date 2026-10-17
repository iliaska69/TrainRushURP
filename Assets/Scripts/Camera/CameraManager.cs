using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class CameraManager : MonoBehaviour
{
    [SerializeField] private UniversalAdditionalCameraData universalAdditionalMainCameraData;
    private DefaultCameraMode _defaultCameraMode;
    private ParkingCameraMode _parkingCameraMode;
    private UIController _uiController;


    async UniTaskVoid WaitForOverlayCameraAsync()
    {
        Camera uiCamera = null;
        while (uiCamera == null)
        {
            uiCamera = _uiController.GetUICamera();
            await UniTask.Yield();
        }

        universalAdditionalMainCameraData.cameraStack.Add(uiCamera);
    }

    private void Start()
    {
        _uiController = UIController.instance;
        WaitForOverlayCameraAsync().Forget();
        _defaultCameraMode = gameObject.GetComponent<DefaultCameraMode>();
        _parkingCameraMode = gameObject.GetComponent<ParkingCameraMode>();
        ActivateDefaultMode();
    }

    public void ActivateDefaultMode()
    {
        _defaultCameraMode.enabled = true;
        _parkingCameraMode.enabled = false;
    }

    public void ActivateParkingMode()
    {
        _defaultCameraMode.enabled = false;
        _parkingCameraMode.enabled = true;
    }

    // public void SetTargetTrailer(Trailer newTarget = null)
    // {
    //     if (Game.instance.GetFirstTrailer() == null) return;
    //     var trailer = newTarget?.transform ?? Game.instance.GetFirstTrailer().transform;
    //     if (trailer == null)
    //     {
    //         Debug.Log("Target for camera is null");
    //         return;
    //     }
    //     gameObject.GetComponent<BaseCameraMode>().SetTargetTrailer(trailer);
    // }
}