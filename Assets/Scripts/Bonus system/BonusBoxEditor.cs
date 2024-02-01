using NaughtyAttributes;
using UnityEngine;

public class BonusBoxEditor : MonoBehaviour
{
    [Range(0, 1)][SerializeField] private float tParameter;


    [Button("Join")]
    public virtual void JoinToCurve()
    {
        var hitColliders = Physics.OverlapSphere(transform.position, 0.6f);
        foreach (var hit in hitColliders)
        {
            var curve = hit.gameObject.GetComponent<BezierCurve>();
            if (curve == null) continue;
            var points = curve.GetAllPoints();

            transform.position = BezierCubic.GetPoint(points[0], points[1], points[2], points[3], tParameter);

            transform.rotation = Quaternion.LookRotation(
                BezierCubic.GetFirstDerivative(points[0], points[1], points[2], points[3], tParameter));
        }
    }
}
