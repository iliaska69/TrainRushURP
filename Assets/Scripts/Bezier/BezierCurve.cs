using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BezierCurve : MonoBehaviour, IBezierCurve
{
    [SerializeField] private PointContainer firstPointContainer;
    [SerializeField] private PointContainer secondPointContainer;
    [Range(0, 1)]
    [SerializeField] private float t;


    void Update()
    {
        transform.position = Bezier.GetPoint(
            firstPointContainer.centerPoint,
            firstPointContainer.secondControlPoint,
            secondPointContainer.firstControlPoint,
            secondPointContainer.centerPoint, t);

        //transform.rotation = Quaternion.LookRotation(
        //    Bezier.GetFirstDerivative(
        //        firstPointContainer.centerPoint,
        //        firstPointContainer.secondControlPoint,
        //        secondPointContainer.firstControlPoint,
        //        secondPointContainer.centerPoint, t));
    }

    public List<Transform> GetAllPoints()
    {
        var points = new List<Transform>
        {
            firstPointContainer.centerPoint,
            firstPointContainer.secondControlPoint,

            secondPointContainer.firstControlPoint,
            secondPointContainer.centerPoint
        };

        return points;
    }
}
