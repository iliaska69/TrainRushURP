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

    private float _curveDistance;

    private void Start()
    {
        CalculateCurveLength();
    }

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

    public void CalculateCurveLength()
    {
        for (float i = 0; i<= 1;  i+=0.05f)
        {
            var a = Bezier.GetPoint(
            firstPointContainer.centerPoint,
            firstPointContainer.secondControlPoint,
            secondPointContainer.firstControlPoint,
            secondPointContainer.centerPoint, i);

            var b = Bezier.GetPoint(
            firstPointContainer.centerPoint,
            firstPointContainer.secondControlPoint,
            secondPointContainer.firstControlPoint,
            secondPointContainer.centerPoint, i + 0.05f);

            _curveDistance += Vector3.Distance(a, b);
        }
    }

    public float GetCurveDistance()
    {
        return _curveDistance;
    }
}
