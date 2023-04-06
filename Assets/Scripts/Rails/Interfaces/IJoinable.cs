using UnityEngine;

public interface IJoinable
{
    void JoinToPoint();

    Vector3 GetDirection();

    GameObject GetJoinablePoint();
}
