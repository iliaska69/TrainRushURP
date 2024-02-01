using UnityEngine;

public static class BezierStraight
{
    public static Vector3 GetPoint(Transform p0, Transform p1, float t)
    {
        t = Mathf.Clamp01(t);
        var oneMinusT = 1f - t;
        return
            oneMinusT * p0.position + t * p1.position;
    }

    public static Vector3 GetFirstDerivative(Transform p0, Transform p1, float t)
    {
        t = Mathf.Clamp01(t);
        var oneMinusT = 1f - t;
        return
            oneMinusT * p0.position + t * p1.position;
    }
}
