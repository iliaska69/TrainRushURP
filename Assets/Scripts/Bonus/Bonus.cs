using UnityEngine;

public class Bonus : MonoBehaviour
{
    protected void OnTriggerEnter(Collider other)
    {
        var hit = other.GetComponent<IImprovable>();
        if (hit == null) return;
        Debug.Log(hit);
        hit.AccelerateSpeed();
        Destroy(gameObject);
    }
}
