using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BezierContainer : MonoBehaviour
{
    [SerializeField] private List<PointContainer> pointContainers;
    
    private void OnDrawGizmos()
    {
        for(var i =0; i< pointContainers.Count - 1; i++)
        { 
            var segmentsNumber = 30;
            var priveousPoint = pointContainers[i].centerPoint.position;

            for (int j = 0; j < segmentsNumber + 1; j++)
            {
                var parameter = (float)j / segmentsNumber;

                var point = Bezier.GetPoint(
                    pointContainers[i].centerPoint,
                    pointContainers[i].secondControlPoint,
                    pointContainers[i + 1].firstControlPoint,
                    pointContainers[i+1].centerPoint,
                    parameter);
                Gizmos.DrawLine(priveousPoint, point);
                priveousPoint = point;
            }
        }
    }
}
