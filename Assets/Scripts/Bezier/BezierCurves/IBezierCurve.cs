using System.Collections.Generic;
using UnityEngine;

public interface IBezierCurve
{
    List<Transform> GetAllPoints();

    void CalculateCurveLength();

    float GetCurveDistance();

    Vector3 GetPositionByPoint(int pointId);
    
    Vector3 GetNormalByPoint(int pointId);

    int GetPointIdByTParametr(float t);

    Vector3 GetRotationByPoint(int pointId);

    bool CheckPoint(int pointId);
}
