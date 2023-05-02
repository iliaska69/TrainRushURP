using UnityEngine;

public interface IJoinable
{
    void JoinToPoint();

    Vector3 GetDirection();

    Vector3 GetFrontPoint();

    void SetFrontPoint(Vector3 point);

    GameObject GameObject { get; }
}
