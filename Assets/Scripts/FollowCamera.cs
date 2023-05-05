using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform camTransform;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime = 1f;

    private Vector3 velocity;
    private Transform _transform; 


    private void Start()
    {
        offset = camTransform.position - target.position;
        _transform = transform;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;

        camTransform.position = Vector3.SmoothDamp(_transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void Update()
    {
        var lookDirection = target.position - camTransform.position;
        lookDirection.Normalize();

        camTransform.rotation = Quaternion.Slerp(
            camTransform.rotation,
            Quaternion.LookRotation(lookDirection),
            smoothTime * 3 * Time.deltaTime);
    }

    public void SetTargetToFollow(Trailer newTraget)
    {
        target = newTraget.transform;
    }
}
