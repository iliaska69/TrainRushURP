using UnityEngine;

public class DefaultCameraMode : BaseCameraMode
{
    private Vector3 defaultPosition = new Vector3(2.1f, 6.86f, -5f);

    private void Start()
    {
        _transform = transform;
        Instantiate();
    }

    private void LateUpdate()
    {
        if (target == null) return;
        Movement();
    }

    private void Update()
    {
        if(target == null && !settingInstantiated)
        {
            SetTargetTrailer();
            InstantiateSettings();
        }
        Rotation();
    }

    private void Movement()
    {
        Debug.Log(target.position);
        Vector3 targetPosition = target.position + offset;
        Debug.Log(_transform.position);
        Debug.Log(targetPosition);
        Debug.Log(smoothTime);
        camTransform.position = Vector3.SmoothDamp(_transform.position, targetPosition, ref velocity, smoothTime);
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
        _transform = transform;
        camTransform.position = defaultPosition;
        defaultRotation = Quaternion.Euler(39f, -14f, 0);
        if (target == null) return;
        offset = camTransform.position - target.position;
        smoothTime = 0.2f;
        settingInstantiated = true;
    }
}
