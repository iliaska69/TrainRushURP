using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteAlways]
public class BezierCurve : MonoBehaviour, IBezierCurve
{
    [SerializeField] protected PointContainer firstPointContainer;
    [SerializeField] protected PointContainer secondPointContainer;
    //[SerializeField] protected int KDistance = 5;
    [SerializeField] protected bool usedFirstVector;
    [SerializeField] protected Vector3 firstVector;
    [Range(0, 1)]
    protected float t = 0;

    protected float _curveDistance;
    protected int _countOfPoints;
    protected int KDistance = 2;
    protected float _step;


    public virtual List<Transform> GetAllPoints()
    {
        throw new NotImplementedException();
    }
    
    public virtual void CalculateCurveLength()
    {
        throw new NotImplementedException();
    }

    public virtual float GetCurveDistance()
    {
        throw new NotImplementedException();
    }

    public virtual Vector3 GetPositionByPoint(int pointId)
    {
        throw new NotImplementedException();
    }

    public virtual Vector3 GetNormalByPoint(int pointId)
    {
        throw new NotImplementedException();
    }
    
    public virtual Vector3 GetNormalByLastPoint()
    {
        throw new NotImplementedException();
    }
    
    public virtual int GetPointIdByTParametr(float t)
    {
        throw new NotImplementedException();
    }

    public virtual Vector3 GetRotationByPoint(int pointId)
    {
        throw new NotImplementedException();
    }

    public virtual bool CheckPoint(int pointId)
    {
        throw new NotImplementedException();
    }

    public int GetPointCount() { return _countOfPoints; }
}
