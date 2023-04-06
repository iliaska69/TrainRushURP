using NaughtyAttributes;
using UnityEngine;

[ExecuteAlways]
public class Rail : MonoBehaviour, IJoinable
{
    [SerializeField] protected GameObject firstPointForDirection;
    [SerializeField] protected GameObject secondPointForDirection;
    [SerializeField] protected GameObject frontPoint;
    [SerializeField] protected GameObject joinablePoint;
    [SerializeField] protected bool openedToMovement;
    [SerializeField] protected GameObject curve;

    public virtual Vector3 GetDirection()
    {
        var direction = secondPointForDirection.transform.position - firstPointForDirection.transform.position;

        return Vector3.Normalize(direction);
    }

    public virtual Vector3 GetSpawnPostionForNextRail()
    {
        return secondPointForDirection.transform.position;
    }

    public virtual GameObject GetJoinablePoint()
    {
        return joinablePoint;
    }

    public virtual bool GetLicenseToMove()
    {
        return openedToMovement;
    }

    public virtual IBezierCurve GetCurve()
    {
        return curve.GetComponent<IBezierCurve>();
    }

    [Button("Join")]
    public virtual void JoinToPoint()
    {
        var hitColliders = Physics.OverlapSphere(frontPoint.transform.position, 0.5f);

        foreach (var hit in hitColliders)
        {
            var rail = hit.gameObject.GetComponent<IJoinable>();
            if (rail != null)
            {
                transform.position = rail.GetJoinablePoint().transform.position;
                transform.rotation = Quaternion.LookRotation(rail.GetDirection());
            }
        }
    }
}