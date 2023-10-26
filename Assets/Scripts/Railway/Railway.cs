using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railway : MonoBehaviour
{
    private List<Rail> rails = new List<Rail>();
    private List<BezierCurve> bezierCurves= new List<BezierCurve>();
    private int _currentCurveId;
    private int _currentPointId;


    void Start()
    {
        ImplementCurvesAndRails();

        _currentCurveId = 0;
        //_currentCurve = bezierCurves[_currentCurveId];
    }

    public Vector3 GetNextPointToMove()
    {
        if (!bezierCurves[_currentCurveId].EstLiTakayaPoint(_currentPointId))
        {
            _currentCurveId++;
        }

        var pos = bezierCurves[_currentCurveId].GetPositionByPoint(_currentPointId);
        _currentPointId++;

        return pos;
    }

    private void ImplementCurvesAndRails()
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
