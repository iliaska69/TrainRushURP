using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform finishTarget;
    private Transform target;


    private void Start()
    {
        ActivateDefaultCameraMode();
        SetTargetTrailer();
    }

    public void ActivateZoomOutCameraMode()
    {
        ClearALlModes();
        gameObject.AddComponent<ZoomOutCameraMode>();
    }

    public void ActivateDefaultCameraMode()
    {
        ClearALlModes();
        gameObject.AddComponent<DefaultCameraMode>();
    }

    public void ActivateParkingCameraMode()
    {
        ClearALlModes();
        gameObject.AddComponent<ParkingCameraMode>().SetTargetTrailer(finishTarget);
    }

    public void SetTargetTrailer(Trailer newTrailer = null)
    {
        target = newTrailer?.transform ?? Game.instance.GetFirstTrailer().transform;
        var cameraMode = gameObject.GetComponent<BaseCameraMode>();
        cameraMode.SetTargetTrailer(target.transform);
    }

    public void ClearALlModes()
    {
        var mode = gameObject.GetComponent<BaseCameraMode>();
        if(mode != null ) 
        Destroy(mode);
    }
}
