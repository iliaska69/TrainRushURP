using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObstacleSave : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO saveEventSO;
    [SerializeField] private List<GameObject> obstalceObjects;
    
    private bool _isUsing;


    private void OnTriggerEnter(Collider other)
    {
        if (_isUsing)
            return;
        if (!other.TryGetComponent<Trailer>(out var trailer)) return;

        if (!trailer.IsMainTrailer()) return;
        saveEventSO.RaiseEvent();
        _isUsing = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isUsing)
            return;
        if (!other.TryGetComponent<Trailer>(out var trailer)) return;

        if (!trailer.IsMainTrailer()) return;
        saveEventSO.RaiseEvent();
        _isUsing = true;
    }
    
    public void HideObstacle()
    {
        if (!_isUsing) return;
        foreach (var obstalceObject in obstalceObjects)
        {
            obstalceObject.SetActive(false);
        }
    }
}
