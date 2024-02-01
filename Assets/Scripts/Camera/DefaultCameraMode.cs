using UnityEngine;

public class DefaultCameraMode : BaseCameraMode
{
    private Vector3 defaultPosition = new Vector3(4.7f, 15.2f, -10.4f);

    private void Start()
    {
        _transform = transform;
        Instantiate();
        InstantiateSettings();
    }

    private void LateUpdate()
    {
        if (_target == null) return;
        Movement();
    }

    private void Update()
    {
        if (!settingInstantiated || _target == null)
        {
            SetTargetTrailer();
            InstantiateSettings();
        }
        Rotation();
    }

    private void Movement()
    {
        Vector3 targetPosition = _target.position + offset;
        //Debug.Log("Current position: " + _target.position);
        //Debug.Log("Target position: " + targetPosition);
        camTransform.position = Vector3.SmoothDamp(
            _transform.position,
            targetPosition, 
            ref velocity,
            smoothTime);
    }

    private void Rotation()
    {
        camTransform.rotation = Quaternion.Slerp(
        camTransform.rotation,
        defaultRotation,
        smoothTime * 5 * Time.deltaTime);
    }

    private void InstantiateSettings()
    {
        if (_target == null) return;
        defaultRotation = Quaternion.Euler(33.41f, -7.4f, 0);
        offset = camTransform.position - _target.position;
        smoothTime = 1f;
        settingInstantiated = true;
    }
}
