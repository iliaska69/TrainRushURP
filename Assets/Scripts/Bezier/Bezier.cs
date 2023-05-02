using UnityEngine;

public static class Bezier
{
    public static Vector3 GetPoint(Transform p0, Transform p1, Transform p2, Transform p3, float t)
    {
        t = Mathf.Clamp01(t);
        var oneMinusT = 1f - t;
        return
            oneMinusT * oneMinusT * oneMinusT * p0.position +
            3f * oneMinusT * oneMinusT * t * p1.position +
            3f * oneMinusT * t * t * p2.position +
            t * t * t * p3.position;
    }

    public static Vector3 GetFirstDerivative(Transform p0, Transform p1, Transform p2, Transform p3, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            3f * oneMinusT * oneMinusT * (p1.position - p0.position) +
            6f * oneMinusT * t * (p2.position - p1.position) +
            3f * t * t * (p3.position - p2.position);
    }
}
