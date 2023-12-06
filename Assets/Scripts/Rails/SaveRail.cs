using UnityEngine;

public class SaveRail : Rail
{
    [SerializeField] private VoidEventChannelSO saveEventSO;
    [SerializeField] private GameObject obstacle;

    private bool _isUsing;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Trailer>(out var trailer)) return;

        if (!trailer.IsMainTrailer()) return;
        saveEventSO.RaiseEvent();
        _isUsing = true;
    }

    public void HideObstacle()
    {
        if(_isUsing)
            obstacle.SetActive(false);
    }
}
