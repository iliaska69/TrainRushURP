using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Rail : MonoBehaviour, IJoinable
{
    [SerializeField] protected Transform firstPointForDirection;
    [SerializeField] protected Transform secondPointForDirection;
    [SerializeField] protected Transform frontPoint;
    //[SerializeField] protected Transform curve;
    [SerializeField] protected bool openedToMovement;

    public GameObject GameObject => gameObject;

    [SerializeField] protected List<BezierCurve> _curves;


    public virtual Vector3 GetDirection()
    {
        var direction = secondPointForDirection.position - firstPointForDirection.position;

        return Vector3.Normalize(direction);
    }

    public virtual Vector3 GetSpawnPostionForNextRail()
    {
        return secondPointForDirection.position;
    }

    public bool GetLicenseToMove()
    {
        return openedToMovement;
    }

    public void SetLicenseToMove(bool license)
    {
        openedToMovement = license;
    }

    public virtual List<BezierCurve> GetCurves()
    {
        return _curves;
    }

    [Button("Join")]
    public virtual void JoinToPoint()
    {
        var hitColliders = Physics.OverlapSphere(frontPoint.transform.position, 0.5f);
        foreach (var hit in hitColliders)
        {
            var point = hit.gameObject.GetComponent<IJoinablePoint>();
            if (point == null) continue;

            var rail = point.GameObject.GetComponentInParent<IJoinable>();
            if (rail == null) continue;

            var joinablePoint = point.GetJoinablePoint();
            transform.position = joinablePoint;
            frontPoint.position = joinablePoint;
            transform.rotation = Quaternion.LookRotation(rail.GetDirection());
        }
    }

    [Button("Fill Curves List")]
    public virtual void FillCurvesList()
    {
        if (_curves != null)
            _curves.Clear();

        var curves = gameObject.GetComponentsInChildren<BezierCurve>();

        
        for (var i = 0; i< curves.Length; i++)
        {
            //Debug.Log(curves[i]);
            _curves.Add(curves[i]);
        }
    }

    public Vector3 GetFrontPoint()
    {
        return frontPoint.position;
    }

    public void SetFrontPoint(Vector3 point)
    {
        frontPoint.position = point;
    }
}