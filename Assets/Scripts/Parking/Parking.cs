using System.Collections.Generic;
using UnityEngine;

public class Parking : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO startParkingEventSO;
    [SerializeField] private VoidEventChannelSO allTrailersParkedEventSO;
    [SerializeField] private VoidEventChannelSO timerParkingFinishParkedEventSO;
    [SerializeField] private List<ParkingPlace> parkingPlaces = new();
    [SerializeField] private float timerForParking = 5f;

    private Trailer _collidedTrailer;
    private bool _isParking;
    private List<Trailer> _trailersForParking;
    private List<Trailer> _trailersThatNotStillParking = new();
    private bool _finishTimer;


    private void Update()
    {
        Timer();
        TrailerParking();
    }

    public void ManageParkingPlace()
    {
        _trailersForParking = Game.instance.GetTrailerList();
        var newParkingPlaces = new List<ParkingPlace>();
        for (var i = 0; i < _trailersForParking.Count; i++)
        {
            if (i >= 16) break;
            newParkingPlaces.Add(parkingPlaces[i]);
        }
        
        var shouldSkip = _trailersForParking.Count - 16;
        var newParkingPlacesIndex = newParkingPlaces.Count-1;
        for (var i = 0; i < _trailersForParking.Count; i++)
        {
            if (i <= shouldSkip-1) continue;
            var parkingComp = _trailersForParking[i].GetTrailerParkingComp();
            parkingComp.SetParkingPlace(newParkingPlaces[newParkingPlacesIndex]);
            newParkingPlacesIndex--;
        }
    }

    public void SetNextTrailerForParking()
    {
        if (_trailersThatNotStillParking.Count == 0) return;

        _trailersThatNotStillParking[0].SetIsReadyForParking(true);
        _trailersThatNotStillParking.RemoveAt(0);

        if (_trailersThatNotStillParking.Count == 0) 
        {
            allTrailersParkedEventSO.RaiseEvent();
        }
    }

    private void Timer()
    {
        if (_finishTimer)
            return;
        if (!_isParking)
            return;
        if (timerForParking <= 0 && !_finishTimer)
        {
            timerParkingFinishParkedEventSO.RaiseEvent();
            Game.instance.VictoryGame();
            _finishTimer = true;
            return;
        }

        timerForParking -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        var trailer = other.GetComponent<Trailer>();
        if (trailer == null) return;
        _collidedTrailer = trailer;
        if (_isParking) return;

        startParkingEventSO.RaiseEvent();
        _isParking = true;
    }

    private void OnTriggerStay(Collider other)
    {
        var trailer = other.GetComponent<Trailer>();
        if (trailer == null) return;
        _collidedTrailer = trailer;
        if (_isParking) return;

        startParkingEventSO.RaiseEvent();
        _isParking = true;
    }

    public void TrailerParking()
    {
        if (_collidedTrailer == null) return;

        if (!_collidedTrailer.IsMainTrailer() || !_collidedTrailer.IsReadyForParking()) return;

        if (_collidedTrailer.GetPreviousTrailer() != null)
            _collidedTrailer.GetPreviousTrailer().SetMainTrailer(true);
        _collidedTrailer.GetTrailerMovementComponent().enabled = false;
        _collidedTrailer.GetTrailerParkingComp().enabled = true;
    }
    
    public void DestroyNotUsedTrailers()
    {
        if (_trailersForParking.Count > 16)
        {
            for (var i = 0; i < _trailersForParking.Count;)
            {
                if (_trailersForParking.Count <= 16) break;
                _trailersForParking[i].DestroyWithoutAnimation();
            }
        }
        for (var i = 0; i < _trailersForParking.Count; i++)
        {
            _trailersThatNotStillParking.Add(_trailersForParking[^(i + 1)]);
        }
    }
}