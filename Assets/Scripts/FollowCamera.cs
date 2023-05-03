using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform camTransform;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime = 0.5f;

    private Vector3 velocity;

    private void Start()
    {
        offset = camTransform.position - target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        camTransform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        Debug.Log(target);
    }

    public void SetTargetToFollow(Trailer newTraget)
    {
        target = newTraget.transform;

    }
}
