using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class SaveRail : Rail
{
    [SerializeField] private VoidEventChannelSO saveEventSO;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Trailer>(out var trailer)) return;

        if (!trailer.IsMainTrailer()) return;

        saveEventSO.RaiseEvent();
    }
}
