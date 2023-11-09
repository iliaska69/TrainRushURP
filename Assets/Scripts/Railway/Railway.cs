using DG.Tweening;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Railway : MonoBehaviour
{
    private List<Rail> rails = new List<Rail>();
    private List<BezierCurve> bezierCurves= new List<BezierCurve>();
    private BezierCurve _currentCurve;

    //Key: id trailer, value: Id curve
    private Dictionary<int, int> _dictionaryOfObjectsOnCurves = new Dictionary<int, int>();
    //Key: id trailer, value: Id point on curve
    private Dictionary<int, int> _dictionaryOfObjectsOnPoints = new Dictionary<int, int>();


    void Awake()
    {
        ImplementCurvesAndRails();
    }

    public Vector3 GetNextPointToMove(int objectId)
    {
        _dictionaryOfObjectsOnCurves.TryGetValue(objectId, out var currentCurveId);
        _dictionaryOfObjectsOnPoints.TryGetValue(objectId, out var currentPointId);
        if (!bezierCurves[currentCurveId].CheckPoint(currentPointId))
        {
            currentCurveId++;
            _dictionaryOfObjectsOnCurves[objectId] = currentCurveId;
            currentPointId = 0;
            _dictionaryOfObjectsOnPoints[objectId] = currentPointId;
        }

        var position = bezierCurves[currentCurveId].GetPositionByPoint(currentPointId);
        currentPointId++;
        _dictionaryOfObjectsOnPoints[objectId] = currentPointId;

        return position;
    }

    public Vector3 GetRotation(int objectId)
    {
        _dictionaryOfObjectsOnCurves.TryGetValue(objectId, out var currentCurveId);
        _dictionaryOfObjectsOnPoints.TryGetValue(objectId, out var currentPointId);

        var rotation = bezierCurves[currentCurveId].GetRotationByPoint(currentPointId);
        
        return rotation;
    }

    public void ImplementObjectToRailway(int objectId, BezierCurve newCurve, float t)
    {
        _currentCurve = newCurve;
        for (var i = 0; i < bezierCurves.Count; i++)
        {
            if (newCurve == bezierCurves[i])
            {
                _dictionaryOfObjectsOnCurves.Add(objectId, i);
                var pointId =  _currentCurve.GetPointIdByTParametr(t);
                _dictionaryOfObjectsOnPoints.Add(objectId, pointId);
            }
        }
    }


    void ImplementCurvesAndRails()
    {
        var railarray = GetComponentsInChildren<Rail>();
        foreach (var rail in railarray)
        {
            rails.Add(rail);
            var newCurves = rail.GetCurves();
            foreach (var curve in newCurves)
            {
                bezierCurves.Add(curve);
            }
        }
    }

    void SetDictionaryOfObjects(int objectId, int curveId)
    {
        _dictionaryOfObjectsOnCurves.Add(objectId, curveId);
    }


}