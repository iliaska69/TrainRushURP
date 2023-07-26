using System.Net;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform camTransform;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime = 5f;

    private Vector3 velocity;
    private Transform _transform;
    private Quaternion branchRotation;
    private Quaternion defaultRotation;
    private bool isBranchView;


    private void Start()
    {
        branchRotation = Quaternion.Euler(53f, -16f, 0);
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
        if (isBranchView)
        {
            camTransform.rotation = Quaternion.Slerp(
            camTransform.rotation,
            branchRotation,
            smoothTime * 3 * Time.deltaTime);
        }
        else
        {
            camTransform.rotation = Quaternion.Slerp(
            camTransform.rotation,
            defaultRotation,
            smoothTime * 5 * Time.deltaTime);
        }
        
        //сделать тоже самое только в другую позицию, но чтобы этот код выполнился и больше не выполянлся
    }

    public void SetTargetToFollow(Trailer newTraget)
    {
        target = newTraget.transform;
    }

    public void SetBranchView(bool value)
    {
        isBranchView = value;
    }
}
