using System.Collections.Generic;
using UnityEngine;

public class Railway : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO finishedLastRailOnRailway;
    private List<Rail> rails = new();
    private List<BezierCurve> bezierCurves = new();

    //Key: id trailer, value: Id curve
    private Dictionary<int, int> _dictionaryOfObjectsOnCurves = new();

    //Key: id trailer, value: Id point on curve
    private Dictionary<int, int> _dictionaryOfObjectsOnPoints = new();

    private float _oneCurvePercentByCurves;

    void Awake()
    {
        ImplementCurvesAndRails();
    }

    void OnEnable()
    {
        _oneCurvePercentByCurves = 1f / bezierCurves.Count;

        Game.instance.SetRailway(this);
    }

    public BezierPointInfo GetNextPointToMove(int objectId)
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
        if (currentPointId >= curvePointCount - 1)
        {
            currentCurveId++;
            _dictionaryOfObjectsOnCurves[objectId] = currentCurveId;
            currentPointId = 0;
            _dictionaryOfObjectsOnPoints[objectId] = currentPointId;
        }

        if (currentCurveId > bezierCurves.Count - 1)
        {
            return new BezierPointInfo();
        }

        BezierPointInfo pointInfo;
        pointInfo.position = bezierCurves[currentCurveId].GetPositionByPoint(currentPointId);
        pointInfo.normal = bezierCurves[currentCurveId].GetNormalByPoint(currentPointId);

        currentPointId++;
        _dictionaryOfObjectsOnPoints[objectId] = currentPointId;

        return pointInfo;
    }

    public Vector3 GetRotation(int objectId)
    {
        return Vector3.zero;
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

    public float GetPercentByObjectId(int objectId)
    {
        _dictionaryOfObjectsOnCurves.TryGetValue(objectId, out var currentCurveId);
        
        return (currentCurveId + 1f) * _oneCurvePercentByCurves;
    }
}