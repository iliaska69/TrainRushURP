using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BezierCurve : MonoBehaviour, IBezierCurve
{
    [SerializeField] private Transform pointZero;
    [SerializeField] private Transform pointOne;
    [SerializeField] private Transform pointTwo;
    [SerializeField] private Transform pointThree;


    [Range(0,1)][SerializeField] private float t;

    void Update()
    {
        transform.position = Bezier.GetPoint(pointZero, pointOne, pointTwo, pointThree, t);

        transform.rotation = Quaternion.LookRotation(
            Bezier.GetFirstDerivative(pointZero, pointOne, pointTwo, pointThree, t));
    }

    public List<Transform> GetAllPoints()
    {
        var points = new List<Transform>
        {
            pointZero,
            pointOne,
            pointTwo,
            pointThree
        };

        return points;
    }

    private void OnDrawGizmos()
    {
        var segmentsNumber = 30;
        var priveousPoint = pointOne.transform.position;

        for (int i = 0; i < segmentsNumber + 1; i++)
        {
            var parameter = (float)i / segmentsNumber;
            var point = Bezier.GetPoint(pointZero, pointOne, pointTwo, pointThree, parameter);
            Gizmos.DrawLine(priveousPoint, point);
            priveousPoint = point;
        }
    }
}
