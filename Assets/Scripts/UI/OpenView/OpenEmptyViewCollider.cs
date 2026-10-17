using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenEmptyViewCollider : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO openEmptyViewEvent;

    private bool _isUsed;
    
    private void OnTriggerEnter(Collider other)
    {
        if (_isUsed) return;
        var trailer =  other.GetComponent<Trailer>();
        if (trailer == null) return;
        openEmptyViewEvent.RaiseEvent();
        _isUsed = true;
    }
}
