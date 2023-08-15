using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOutCameraMode : DefaultCameraMode
{
    private void Start()
    {
        Instantiate();
        camTransform.position = new Vector3(1f, 18.4f, 27.5f);
        defaultRotation = Quaternion.Euler(49.3f, -11.1f, 0);
        offset = camTransform.position - target.position;
        _transform = transform;
        smoothTime = 0.2f;
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
