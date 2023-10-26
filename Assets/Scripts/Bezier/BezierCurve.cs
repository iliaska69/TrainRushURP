using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class BezierCurve : MonoBehaviour, IBezierCurve
{
    [SerializeField] private PointContainer firstPointContainer;
    [SerializeField] private PointContainer secondPointContainer;
    [Range(0, 1)]
    private float t = 0;

    private float _curveDistance;
    private int _countOfPoints;
    private int _kDistance = 5;
    private float _step;


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

        _countOfPoints = (int)_curveDistance * _kDistance;
        _step = 1f / _countOfPoints;
    }

    public float GetCurveDistance()
    {
        return _curveDistance;
    }

    public Vector3 GetPositionByPoint(int pointId)
    {
        if (pointId >= _countOfPoints)
            return Vector3.zero;

        t = _step* pointId;
        var pos = Bezier.GetPoint(
            firstPointContainer.centerPoint,
            firstPointContainer.secondControlPoint,
            secondPointContainer.firstControlPoint,
            secondPointContainer.centerPoint, t);
       
        return pos;
    }

    public bool EstLiTakayaPoint(int pointId)
    {
        return pointId < _countOfPoints && pointId >= 0;
    }
}
