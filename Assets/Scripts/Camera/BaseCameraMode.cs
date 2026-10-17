using UnityEngine;

public class BaseCameraMode : MonoBehaviour
{
    [SerializeField] protected float smoothMovement;
    [SerializeField] protected float maxSmoothMovement;

    protected Transform _trailerTarget;
    protected bool _isPositionFixed;
    protected Transform _camTransform;
    protected Vector3 _offset;
    protected Vector3 _velocity;
    protected bool _settingInstantiated;
    protected Transform _cameraTransformFormTrigger;
    protected Quaternion _cameraRotationFormTrigger;
    protected bool _isCameraRotationFixed;
    protected Vector3 _cameraMoveDirection;
    
    

    protected void Instantiate(Trailer newTrailer = null)
    {
        SetCamera();
        SetTargetTrailer(newTrailer);
    }

    public void SetTargetTrailer(Trailer newTarget = null)
    {
        if (Game.instance.GetMainTrailerFromTrailers() == null) return;
        _trailerTarget = newTarget?.GetCameraFollowingTarget() ?? Game.instance.GetMainTrailerFromTrailers().GetCameraFollowingTarget();
    }

    public void SetCamera()
    {
        _camTransform = gameObject.GetComponent<Camera>().gameObject.transform;
    }

    public void SetCameraSettings(CameraSettings cameraSettings)
    {
        var cameraPosition = cameraSettings.CameraTransform;
        _offset = cameraPosition.position - _trailerTarget.position;
        _isPositionFixed = cameraSettings.IsCameraPositionFixed;
        _cameraTransformFormTrigger = cameraPosition;
        _cameraTransformFormTrigger.position = new Vector3(
                cameraSettings.CameraTransform.position.x,
                cameraSettings.CameraTransform.position.y,
                cameraSettings.CameraTransform.position.z);
        
        _isCameraRotationFixed = cameraSettings.IsCameraRotationFixed;
        _cameraRotationFormTrigger = cameraSettings.CameraRotation;
        _cameraMoveDirection = cameraSettings.CameraMoveDirection;
    }
}