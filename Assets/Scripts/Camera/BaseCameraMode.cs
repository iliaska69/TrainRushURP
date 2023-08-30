using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCameraMode : MonoBehaviour
{
    protected Transform target;
    protected Transform camTransform;
    protected Vector3 offset;
    protected float smoothTime;
    protected Vector3 velocity;
    protected Transform _transform;
    protected Quaternion defaultRotation;
    protected bool settingInstantiated;

    protected void Instantiate(Trailer newTrailer = null)
    {
        SetCamera();
        SetTargetTrailer(newTrailer?.transform);
    }

    public void SetTargetTrailer(Transform newTarget = null)
    {
        if (Game.instance.GetFirstTrailer() == null) return;
        target = newTarget?.transform ?? Game.instance.GetFirstTrailer().transform;
    }


    public void SetCamera()
    {
        camTransform = gameObject.GetComponent<Camera>().gameObject.transform;
    }
}
