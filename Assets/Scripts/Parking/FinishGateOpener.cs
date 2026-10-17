using System;
using DG.Tweening;
using UnityEngine;

public class FinishGateOpener : MonoBehaviour
{
    [SerializeField] private GameObject gate;
    [SerializeField] private float duration;
    private bool _isUsed;

    private void OnTriggerEnter(Collider other)
    {
        if (_isUsed) return;
        
        var trailer = other.GetComponent<Trailer>();
        if (trailer == null) return;
        OpenGate();
        _isUsed = true;
    }

    private void OpenGate()
    {
        gate.transform.DOLocalRotate(new Vector3(0, 0, -90f), duration);
    }
}
