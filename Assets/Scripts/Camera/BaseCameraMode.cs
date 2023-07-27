using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCameraMode : MonoBehaviour
{
    [SerializeField] protected Transform target;
    [SerializeField] protected Transform camTransform;
    [SerializeField] protected Vector3 offset;
    [SerializeField] protected float smoothTime = 5f;

    protected Vector3 velocity;
    protected Transform _transform;
    protected Quaternion defaultRotation;


    public void SetTargetToFollow(Trailer newTraget)
    {
        target = newTraget.transform;
    }

    public void SetCamera()
    {
        camTransform = gameObject.GetComponent<Camera>().gameObject.transform;
    }
}
