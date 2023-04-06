using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class UnbranchingRail : Rail
{
    public override void JoinToPoint()
    {
        var hitColliders = Physics.OverlapSphere(transform.position, 1f);
        var joinablePoints = new List<Vector3>();
        var sum = Vector3.zero;

        foreach (var hit in hitColliders)
        {
            if (hitColliders.Length != 2) { return; }

            var rail = hit.gameObject.GetComponent<IJoinable>();
            if (rail != null)
            {
                joinablePoints.Add(rail.GetJoinablePoint().transform.position);
                
            }
        }

        if(joinablePoints.Count != 2) { return; }

        foreach (var vec in joinablePoints)
        {
            sum += vec;
        }

        var center = sum / joinablePoints.Count;
        transform.position = center;
        //transform.rotation = Quaternion.LookRotation(rail.GetDirection());
    }
}