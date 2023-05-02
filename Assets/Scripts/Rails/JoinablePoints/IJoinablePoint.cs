using UnityEngine;

public interface IJoinablePoint
{
    Vector3 GetJoinablePoint();

    GameObject GameObject { get; }
}
