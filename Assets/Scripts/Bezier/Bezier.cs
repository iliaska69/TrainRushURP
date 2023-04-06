using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bezier
{
    public static Vector3 GetPoint(GameObject p0, GameObject p1, GameObject p2, GameObject p3, float t)
    {
        t = Mathf.Clamp01(t);
        var oneMinusT = 1f - t;
        return
            oneMinusT * oneMinusT * oneMinusT * p0.transform.position +
            3f * oneMinusT * oneMinusT * t * p1.transform.position +
            3f * oneMinusT * t * t * p2.transform.position +
            t * t * t * p3.transform.position;
    }

    public static Vector3 GetFirstDerivative(GameObject p0, GameObject p1, GameObject p2, GameObject p3, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            3f * oneMinusT * oneMinusT * (p1.transform.position - p0.transform.position) +
            6f * oneMinusT * t * (p2.transform.position - p1.transform.position) +
            3f * t * t * (p3.transform.position - p2.transform.position);
    }
}
