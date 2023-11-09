using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BezierCurve : MonoBehaviour, IBezierCurve
{
    [SerializeField] protected PointContainer firstPointContainer;
    [SerializeField] protected PointContainer secondPointContainer;
    [Range(0, 1)]
    protected float t = 0;

    protected float _curveDistance;
    protected int _countOfPoints;
    protected int _kDistance = 5;
    protected float _step;


    //private void Awake()
    //{
    //    CalculateCurveLength();
    //}

    //void Update()
    //{
    //    transform.position = BezierCubic.GetPoint(
    //        firstPointContainer.centerPoint,
    //        firstPointContainer.secondControlPoint,
    //        secondPointContainer.firstControlPoint,
    //        secondPointContainer.centerPoint, t);
    //}

    public virtual List<Transform> GetAllPoints()
    {
        throw new NotImplementedException();
        //var points = new List<Transform>
        //{
        //    firstPointContainer.centerPoint,
        //    firstPointContainer.secondControlPoint,

        //    secondPointContainer.firstControlPoint,
        //    secondPointContainer.centerPoint
        //};

        //return points;
    }

    public virtual void CalculateCurveLength()
    {
        throw new NotImplementedException();
        //for (float i = 0; i<= 1;  i+=0.05f)
        //{
        //    var a = BezierCubic.GetPoint(
        //    firstPointContainer.centerPoint,
        //    firstPointContainer.secondControlPoint,
        //    secondPointContainer.firstControlPoint,
        //    secondPointContainer.centerPoint, i);

        //    var b = BezierCubic.GetPoint(
        //    firstPointContainer.centerPoint,
        //    firstPointContainer.secondControlPoint,
        //    secondPointContainer.firstControlPoint,
        //    secondPointContainer.centerPoint, i + 0.05f);

        //    _curveDistance += Vector3.Distance(a, b);
        //}

        //_countOfPoints = (int)_curveDistance * _kDistance;
        //_step = 1f / _countOfPoints;
    }

    public virtual float GetCurveDistance()
    {
        throw new NotImplementedException();
        //return _curveDistance;
    }

    public virtual Vector3 GetPositionByPoint(int pointId)
    {
        throw new NotImplementedException();
        //if (pointId >= _countOfPoints)
        //    return Vector3.zero;

        //t = _step * pointId;
        //var pos = BezierCubic.GetPoint(
        //    firstPointContainer.centerPoint,
        //    firstPointContainer.secondControlPoint,
        //    secondPointContainer.firstControlPoint,
        //    secondPointContainer.centerPoint, t);

        //return pos;
    }

    public virtual int GetPointIdByTParametr(float t)
    {
        throw new NotImplementedException();
        //var pointId = t / _step;

        //return Convert.ToInt32(pointId);
    }

    public virtual Vector3 GetRotationByPoint(int pointId)
    {
        throw new NotImplementedException();
        //if (pointId > _countOfPoints)
        //    return Vector3.zero;

        //t = _step * pointId;
        //var rotation = BezierCubic.GetFirstDerivative(
        //    firstPointContainer.centerPoint,
        //    firstPointContainer.secondControlPoint,
        //    secondPointContainer.firstControlPoint,
        //    secondPointContainer.centerPoint, t);

        //return rotation;
    }

    public virtual bool CheckPoint(int pointId)
    {
        throw new NotImplementedException();
        //return pointId < _countOfPoints && pointId >= 0;
    }

}
