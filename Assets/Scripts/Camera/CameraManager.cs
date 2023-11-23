using UnityEngine;


public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform finishTarget;


    private void Start()
    {
        ActivateDefaultCameraMode();
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

    public void ClearALlModes()
    {
        var mode = gameObject.GetComponent<BaseCameraMode>();
        if(mode != null ) 
        Destroy(mode);
    }

    public void SetTargetTrailer(Trailer newTarget = null)
    {
        if (Game.instance.GetFirstTrailer() == null) return;
        var trailer = newTarget?.transform ?? Game.instance.GetFirstTrailer().transform;
        if (trailer == null)
        {
            Debug.Log("Target for camera is null");
            return;
        }
        gameObject.GetComponent<BaseCameraMode>().SetTargetTrailer(trailer);
    }
}
