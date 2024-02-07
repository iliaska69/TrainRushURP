using System;
using System.Collections.Generic;
using UnityEngine;

public class BezierStraightCurve : BezierCurve
{
    void Awake()
    {
        CalculateCurveLength();
    }

    void Update()
    {
        transform.position = BezierStraight.GetPoint(
            firstPointContainer.centerPoint,
            secondPointContainer.centerPoint,
            t);
    }

    public override List<Transform> GetAllPoints()
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

    public override void CalculateCurveLength()
    {
        for (float i = 0; i <= 1; i += 0.05f)
        {
            var a = BezierStraight.GetPoint(
            firstPointContainer.centerPoint,
            secondPointContainer.centerPoint,
            i);

            var b = BezierStraight.GetPoint(
            firstPointContainer.centerPoint,
            secondPointContainer.centerPoint,
            i + 0.05f);

            _curveDistance += Vector3.Distance(a, b);
        }

        _countOfPoints = (int)_curveDistance * _kDistance;
        _step = 1f / _countOfPoints;
    }

    public override float GetCurveDistance()
    {
        return _curveDistance;
    }

    public override Vector3 GetPositionByPoint(int pointId)
    {
        if (pointId >= _countOfPoints)
            return Vector3.zero;

        var t = _step * pointId;
        var pos = BezierStraight.GetPoint(
            firstPointContainer.centerPoint,
            secondPointContainer.centerPoint,
            t);
        
        return pos;
    }

    public override int GetPointIdByTParametr(float t)
    {
        var pointId = t / _step;

        return Convert.ToInt32(pointId);
    }

    public override Vector3 GetRotationByPoint(int pointId)
    {
        if (pointId > _countOfPoints)
            return Vector3.zero;

        t = _step * pointId;
        var rotation = BezierStraight.GetFirstDerivative(
            firstPointContainer.centerPoint,
            secondPointContainer.centerPoint,
            t);

        return rotation;
    }

    public override bool CheckPoint(int pointId)
    {
        return pointId < _countOfPoints && pointId >= 0;
    }
}
