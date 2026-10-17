using System.Collections.Generic;
using UnityEngine;

public class CleanerNotUsedTrailer : MonoBehaviour
{
    [SerializeField] private Parking parking;
    private List<Trailer> trailers;
    private bool _isUsed;

    private void OnTriggerEnter(Collider other)
    {
        if (_isUsed) return;
        
        var trailer = other.GetComponent<Trailer>();
        if (trailer == null) return;
        
        parking.DestroyNotUsedTrailers();
        _isUsed = true;
    }

    
}