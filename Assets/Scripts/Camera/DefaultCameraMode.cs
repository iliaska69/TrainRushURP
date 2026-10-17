using UnityEngine;

public class DefaultCameraMode : BaseCameraMode
{
    [Header("Movement")]
    [SerializeField] private float smoothMovementStep;
    [SerializeField] private float minMovementStep;
    [SerializeField] private float maxMovementStep;
    [SerializeField] private float smoothAccelerationMovementStep;
    [SerializeField] private float smoothBrakeMovementStep;
    
    [Header("Rotation")]
    [SerializeField] private float maxRotationSmooth;
    [SerializeField] private float minRotationSmooth;
    [SerializeField] private float rotationSmooth;
    [SerializeField] private float angleThreshold = 30.0f;
    [SerializeField] private float smoothAccelerationRotationStep;
    [SerializeField] private float smoothBrakeRotationStep;
    
    private bool _positionAlreadyFixed;
    private float _currentSmooth = 0;

    private void Start()
    {
        Instantiate();
        InstantiateCameraSettings();
    }

    private void LateUpdate()
    {
        if (_trailerTarget == null) return;
        
        if (_cameraMoveDirection != Vector3.zero)
        {
            AxisMovement();
        }
        else
        {
            Movement();
        }
        Rotation();
    }

    private void Update()
    {
        if (!_settingInstantiated || _trailerTarget == null)
        {
            SetTargetTrailer();
            InstantiateCameraSettings();
        }
    }

    private void Movement()
    {
        Vector3 targetPosition;
        if (_isPositionFixed)
            targetPosition = _cameraTransformFormTrigger.position;
        else
            targetPosition = _trailerTarget.position + _offset;

        var distance = Vector3.Distance(_camTransform.position, targetPosition);
        if (distance > 15)
        {
            smoothMovementStep += smoothAccelerationMovementStep * Time.deltaTime;
        }
        else if (distance <= 4)
        {
            smoothMovementStep -= smoothBrakeMovementStep * Time.deltaTime;
        }

        smoothMovementStep = Mathf.Clamp(smoothMovementStep, minMovementStep, maxMovementStep);
        _camTransform.position
            = Vector3.Lerp(_camTransform.position, targetPosition, smoothMovementStep * Time.deltaTime);
    }
    
    private void AxisMovement()
    {
        var targetPosition = _trailerTarget.position + _offset;
        _camTransform.position += new Vector3(
            _cameraMoveDirection.x * (targetPosition.x - _camTransform.position.x),
            _cameraMoveDirection.y * (targetPosition.y - _camTransform.position.y),
            _cameraMoveDirection.z * (targetPosition.z - _camTransform.position.z)
        ) * (0.5f * Time.deltaTime);
    }
    
    private void Rotation()
    {
        Quaternion targetRotation;
        if (_isCameraRotationFixed)
            targetRotation = _cameraTransformFormTrigger.rotation;
        else
            targetRotation = Quaternion.LookRotation(_trailerTarget.position - _camTransform.position);
        
        var angleDifference = Quaternion.Angle(_camTransform.rotation, targetRotation);
        if (angleDifference > angleThreshold)
        {
            rotationSmooth += smoothAccelerationRotationStep * Time.deltaTime;
        }
        else if (angleDifference <= 3)
        {
            rotationSmooth -= smoothBrakeRotationStep * Time.deltaTime;
        }
        rotationSmooth = Mathf.Clamp(rotationSmooth, minRotationSmooth, maxRotationSmooth);
        _currentSmooth = Mathf.Lerp(_currentSmooth, rotationSmooth, 0.5f);
        
        
        _camTransform.rotation =
            Quaternion.Lerp(_camTransform.rotation, targetRotation, _currentSmooth * Time.deltaTime);
    }
    
    private void InstantiateCameraSettings()
    {
        if (_trailerTarget == null) return;
        _offset = _camTransform.position - _trailerTarget.position;
        _settingInstantiated = true;
        var direction = _trailerTarget.position - _camTransform.position;
        _camTransform.rotation = Quaternion.LookRotation(direction);
        rotationSmooth = minRotationSmooth;
    }

    public void DeactivateTrailerTarget()
    {
        _trailerTarget = null;
    }
}