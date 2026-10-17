using System.Collections.Generic;
using UnityEngine;

public class UnbranchingRail : Rail
{
    [SerializeField] private Rail leftRail;
    [SerializeField] private Rail rightRail;


    public override void JoinToPoint()
    {
        var leftFronPoint = leftRail.GetFrontPoint();
        var rightFronPoint = rightRail.GetFrontPoint();

        var hitLeftColliders = Physics.OverlapSphere(leftFronPoint, 0.5f);
        var hitRightColliders = Physics.OverlapSphere(rightFronPoint, 0.5f);

        var joinablePoints = new List<Vector3>();
        var sum = Vector3.zero;

        foreach (var hit in hitLeftColliders)
        {
            if (!hit.gameObject.TryGetComponent<IJoinablePoint>(out var point)) continue;

            joinablePoints.Add(point.GetJoinablePoint());
        }

        foreach (var hit in hitRightColliders)
        {
            if (!hit.gameObject.TryGetComponent<IJoinablePoint>(out var point)) continue;

            joinablePoints.Add(point.GetJoinablePoint());
        }

        if (joinablePoints.Count != 2)
            return;

        foreach (var vector in joinablePoints)
        {
            sum += vector;
        }

        var center = sum / joinablePoints.Count;

        transform.position = center;
        leftRail.SetFrontPoint(joinablePoints[0]);
        rightRail.SetFrontPoint(joinablePoints[1]);
    }
}