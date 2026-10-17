using UnityEngine;

public class ParkingCameraMode : BaseCameraMode
{
    [SerializeField] private Trailer finishTarget;
    [SerializeField] private Transform cameraPlacePosition;
    [SerializeField] private float cameraRotationSmooth;

    private void Start()
    {
        //_transform = transform;
        SetCamera();
        SetTargetTrailer(finishTarget);
    }

    private void LateUpdate()
    {
        if (_trailerTarget == null) return;
        Movement();
    }

    private void Movement()
    {
        _camTransform.position 
            = Vector3.Lerp(_camTransform.position, cameraPlacePosition.position, smoothMovement * Time.deltaTime);
        // _camTransform.position = Vector3.SmoothDamp(
        //     _camTransform.position,
        //     cameraPlacePosition.position,
        //     ref _velocity,
        //     smoothMovement);
    }

    private void Update()
    {
        // if (Vector3.Distance(camTransform.position,cameraPlacePosition.position ) < 0.05f)
        //     return;
        Rotation();
    }

    private void Rotation()
    {
        _camTransform.rotation =
            Quaternion.Lerp(_camTransform.rotation, cameraPlacePosition.rotation, 3 * Time.deltaTime);
        // var targetDirection = finishTarget.transform.position - _camTransform.position;
        //
        // var smoothDirection = Vector3.SmoothDamp(
        //     _camTransform.forward,
        //     targetDirection,
        //     ref _currentVelocity,
        //     cameraRotationSmooth
        // );
        //
        // _camTransform.rotation = Quaternion.LookRotation(smoothDirection);
    }
}