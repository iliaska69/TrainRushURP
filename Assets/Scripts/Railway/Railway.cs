using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Railway : MonoBehaviour
{
    private List<Rail> rails = new List<Rail>();
    private List<BezierCurve> bezierCurves = new List<BezierCurve>();

    //Key: id trailer, value: Id curve
    private Dictionary<int, int> _dictionaryOfObjectsOnCurves = new Dictionary<int, int>();
    //Key: id trailer, value: Id point on curve
    private Dictionary<int, int> _dictionaryOfObjectsOnPoints = new Dictionary<int, int>();


    private void Update()
    {
        //Debug.Log(_dictionaryOfObjectsOnPoints.Count);
    }

    void Awake()
    {
        ImplementCurvesAndRails();
    }

    void Start()
    {
        Game.instance.SetRailway(this);
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
        var curvePointCount = bezierCurves[currentCurveId].GetPointCount();
        if (currentPointId >= curvePointCount - 5)
        {
            currentCurveId++;
            _dictionaryOfObjectsOnCurves[objectId] = currentCurveId;
            currentPointId = 0;
            _dictionaryOfObjectsOnPoints[objectId] = currentPointId;
        }
        var position = bezierCurves[currentCurveId].GetPositionByPoint(currentPointId);
        currentPointId++;
        _dictionaryOfObjectsOnPoints[objectId] = currentPointId;

        //Debug.Log("Curve ID " + currentCurveId);
        //Debug.Log("Point ID " + currentPointId);

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
        for (var i = 0; i < bezierCurves.Count; i++)
        {
            if (newCurve == bezierCurves[i])
            {
                _dictionaryOfObjectsOnCurves.Add(objectId, i);
                var pointId = newCurve.GetPointIdByTParametr(t);
                _dictionaryOfObjectsOnPoints.Add(objectId, pointId);
            }
        }
    }

    public Vector3 GetPositionByT(BezierCurve newCurve, float t)
    {
        var pointId = newCurve.GetPointIdByTParametr(t);
        return newCurve.GetPositionByPoint(pointId);
    }

    public Dictionary<int, int> GetDictionaryOfObjectOnCurves()
    {
        return _dictionaryOfObjectsOnCurves;
    }

    public void SetDictionaryOfObjectOnCurves(int key, int value)
    {
        if (_dictionaryOfObjectsOnCurves.ContainsKey(key))
            _dictionaryOfObjectsOnCurves[key] = value;
        else
            _dictionaryOfObjectsOnCurves.Add(key, value);
    }

    public Dictionary<int, int> GetDictionaryOfObjectOnPoints()
    {
        return _dictionaryOfObjectsOnPoints;
    }

    public void SetDictionaryOfObjectOnPoints(int key, int value)
    {
        if (_dictionaryOfObjectsOnPoints.ContainsKey(key))
            _dictionaryOfObjectsOnPoints[key] = value;
        else
            _dictionaryOfObjectsOnPoints.Add(key, value);
    }

    public void ClearRailwayObjects()
    {
        _dictionaryOfObjectsOnCurves.Clear();
        _dictionaryOfObjectsOnPoints.Clear();
    }

    public void DeleteKeyFromDictionaries(int key)
    {
        if (_dictionaryOfObjectsOnCurves.ContainsKey(key) && _dictionaryOfObjectsOnPoints.ContainsKey(key))
        {
            _dictionaryOfObjectsOnCurves.Remove(key);
            _dictionaryOfObjectsOnPoints.Remove(key);
        }
        else
            Debug.Log("This key not contains in dictionaries");
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
}