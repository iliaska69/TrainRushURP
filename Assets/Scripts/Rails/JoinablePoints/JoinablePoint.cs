using UnityEngine;

public class JoinablePoint : MonoBehaviour, IJoinablePoint
{
    public GameObject GameObject => transform.gameObject;

    public Vector3 GetJoinablePoint()
    {
        return transform.position;
    }
}
