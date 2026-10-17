using UnityEngine;
using UnityEngine.Serialization;

public class TomatoObstacle : MonoBehaviour
{
    [SerializeField] private Transform physicPoint;
    
    private void OnTriggerEnter(Collider other)
    {
        var hit = other.GetComponent<IDestroyable>();
        if (hit == null) return;
        hit.DestroyWithAnimation(physicPoint);
    }
}
