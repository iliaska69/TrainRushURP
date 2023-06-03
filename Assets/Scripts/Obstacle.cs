using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var hit = other.GetComponent<IDestroyable>();
        if (hit == null) return;
        Debug.Log(hit);
        hit.Destroy();
    }
}
