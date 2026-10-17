using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParkingPlace : MonoBehaviour
{
    private List<BezierCurve> _bezierCurves = new();
    private int currentCurveId;
    private int currentPointId;
    private int pointIdCount;
    
    
    private void Start()
    {
        ImplementCurves();
    }

    private void ImplementCurves()
    {
        _bezierCurves = GetComponentsInChildren<BezierCurve>().ToList();
    }

    public BezierPointInfo GetNextPointToMove()
    {
        if (!_bezierCurves[currentCurveId].CheckPoint(currentPointId))
        {
            currentCurveId++;
            currentPointId = 0;
        }
        
        var curvePointCount =_bezierCurves[currentCurveId].GetPointCount();
        if (currentPointId >= curvePointCount - 1)
        {
            currentCurveId++;
            currentPointId = 0;
        }
        
        if (currentCurveId > _bezierCurves.Count - 1)
        {
            Game.instance.IncrementCountOfFinishedTrailer();
            return new BezierPointInfo();
        }
        
        BezierPointInfo pointInfo;
        pointInfo.position = _bezierCurves[currentCurveId].GetPositionByPoint(currentPointId);
        pointInfo.normal = _bezierCurves[currentCurveId].GetNormalByPoint(currentPointId);
        
        currentPointId++;

        return pointInfo;
    }
}
