using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCubicCurve : BezierCurve
{
    [SerializeField] private float normalLength = 2.0f;
    
    Vector3 _prevTangent = Vector3.zero;
    Vector3 _prevNormal = Vector3.zero;
    Vector3 _prevBinormal = Vector3.zero;
    
    
    void Awake()
    {
        CalculateCurveLength();
    }

    void OnDrawGizmos()
    {
        Vector3 prevTangent = Vector3.zero;
        Vector3 prevNormal = Vector3.zero;
        Vector3 prevBinormal = Vector3.zero;
        
    
        for (var point = 0; point < _countOfPoints - 1; point++)
        {
            var currentPointPos = GetPositionByPoint(point);
            var nextPointPos = GetPositionByPoint(point + 1);
            var direction = (nextPointPos - currentPointPos).normalized;
    
            Vector3 tangent;
            Vector3 normal;
            Vector3 binormal;
           
            
            if (point == 0)
            {
                tangent = direction;
                
                if (usedFirstVector)
                {
                    normal = Vector3.Cross(firstVector, tangent).normalized;
                }
                else
                {
                    normal = Vector3.Cross(firstVector, tangent).normalized;
                    if (normal == Vector3.zero)
                    {
                        normal = Vector3.Cross(Vector3.forward, tangent).normalized;
                    }
                }
                
                binormal = Vector3.Cross(tangent, normal).normalized;
            }
            else
            {
                tangent = direction;
                normal = Vector3.Cross(prevBinormal, tangent).normalized;
                binormal = Vector3.Cross(tangent, normal).normalized;
            }
    
            normal = -normal;
    
            prevTangent = tangent;
            prevNormal = normal;
            prevBinormal = binormal;
    
            Gizmos.color = Color.red;
            Gizmos.DrawLine(currentPointPos, currentPointPos + normal * normalLength);
        }
    }

    void Update()
    {
        transform.position = BezierCubic.GetPoint(
            firstPointContainer.centerPoint,
            firstPointContainer.secondControlPoint,
            secondPointContainer.firstControlPoint,
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
            var a = BezierCubic.GetPoint(
                firstPointContainer.centerPoint,
                firstPointContainer.secondControlPoint,
                secondPointContainer.firstControlPoint,
                secondPointContainer.centerPoint,
                i);

            var b = BezierCubic.GetPoint(
                firstPointContainer.centerPoint,
                firstPointContainer.secondControlPoint,
                secondPointContainer.firstControlPoint,
                secondPointContainer.centerPoint,
                i + 0.05f);

            _curveDistance += Vector3.Distance(a, b);
        }

        _countOfPoints = (int)_curveDistance * KDistance;
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
        var pos = BezierCubic.GetPoint(
            firstPointContainer.centerPoint,
            firstPointContainer.secondControlPoint,
            secondPointContainer.firstControlPoint,
            secondPointContainer.centerPoint,
            t);

        return pos;
    }

    public override Vector3 GetNormalByPoint(int pointId)
    {
        var currentPointPos = GetPositionByPoint(pointId);
        var nextPointPos = GetPositionByPoint(pointId + 1);
        var direction = (nextPointPos - currentPointPos).normalized;

        Vector3 tangent;
        Vector3 normal;
        Vector3 binormal;

        if (pointId == 0)
        {
            tangent = direction;
                
            if (usedFirstVector)
            {
                normal = Vector3.Cross(firstVector, tangent).normalized;
            }
            else
            {
                normal = Vector3.Cross(firstVector, tangent).normalized;
                if (normal == Vector3.zero)
                {
                    normal = Vector3.Cross(Vector3.forward, tangent).normalized;
                }
            }

            binormal = Vector3.Cross(tangent, normal).normalized;
        }
        else
        {
            tangent = direction;
            normal = Vector3.Cross(_prevBinormal, tangent).normalized;
            binormal = Vector3.Cross(tangent, normal).normalized;
        }

        normal = -normal;

        _prevTangent = tangent;
        _prevNormal = normal;
        _prevBinormal = binormal;

        return normal;  
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
        var rotation = BezierCubic.GetFirstDerivative(
            firstPointContainer.centerPoint,
            firstPointContainer.secondControlPoint,
            secondPointContainer.firstControlPoint,
            secondPointContainer.centerPoint,
            t);

        return rotation;
    }

    public override bool CheckPoint(int pointId)
    {
        return pointId < _countOfPoints && pointId >= 0;
    }
}