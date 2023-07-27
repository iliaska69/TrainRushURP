using UnityEngine;

public class DefaultCameraMode : BaseCameraMode
{
    private void Start()
    {

        SetCamera();
        camTransform.position = new Vector3(2.1f, 6.86f, -5f);
        defaultRotation = Quaternion.Euler(39f, -14f, 0);
        offset = camTransform.position - target.position;
        _transform = transform;
    }

    private void LateUpdate()
    {
        if (target == null) return;
        Vector3 targetPosition = target.position + offset;

        camTransform.position = Vector3.SmoothDamp(_transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void Update()
    {
        
        camTransform.rotation = Quaternion.Slerp(
        camTransform.rotation,
        defaultRotation,
        smoothTime * 5 * Time.deltaTime);
    }
}
