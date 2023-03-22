using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BezierCurve : MonoBehaviour
{
    [SerializeField] private GameObject pointZero;
    [SerializeField] private GameObject pointOne;
    [SerializeField] private GameObject pointTwo;
    [SerializeField] private GameObject pointThree;


    [Range(0,1)][SerializeField] private float t;

    void Update()
    {
        transform.position = Bezier.GetPoint(pointZero, pointOne, pointTwo, pointThree, t);

        transform.rotation = Quaternion.LookRotation(
            Bezier.GetFirstDerivative(pointZero, pointOne, pointTwo, pointThree, t));
    }

    private void OnDrawGizmos()
    {
        var sigmentsNumber = 30;
        var preveousPoint = pointOne.transform.position;

        for (int i = 0; i < sigmentsNumber + 1; i++)
        {
            var parameter = (float)i / sigmentsNumber;
            var point = Bezier.GetPoint(pointZero, pointOne, pointTwo, pointThree, parameter);
            Gizmos.DrawLine(preveousPoint, point);
            preveousPoint = point;
        }
    }
}
