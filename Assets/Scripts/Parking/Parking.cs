using System.Collections.Generic;
using UnityEngine;

public class Parking : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO startParkingEventSO;
    [SerializeField] private List<ParkingPlace> parkingPlaces= new List<ParkingPlace>();
    [SerializeField] private float timerForParking = 5f;

    private Trailer _readyForParkingTrailer;
    private bool _isParking;


    private void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if (!_isParking) return;

        if (timerForParking <= 0)
        {
            _isParking = false;
            Game.instance.VictoryGame();
        }
        
        timerForParking -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        var trailer = other.GetComponent<Trailer>();
        if (trailer == null) return;

        if (!_isParking)
        {
            startParkingEventSO.RaiseEvent();
            _isParking = true;
        }

        if (!trailer.IsMainTrailer()) return;

        SetTrailerReadyForParking(trailer);
    }

    public void TrailerParking()
    {
        if (!_isParking) return;

        if (_readyForParkingTrailer == null) return;

        _readyForParkingTrailer.GetTrailerMovementComponent().enabled = false;

        if (_readyForParkingTrailer.GetPreviousTrailer() != null)
            _readyForParkingTrailer.GetPreviousTrailer().SetMainTrailer(true);

        _readyForParkingTrailer.GetComponent<TrailerParking>().enabled = true;
        
    }

    public void SetTrailerReadyForParking(Trailer trailer)
    {
        _readyForParkingTrailer = trailer;
    }

    public Trailer GetReadyForParkingTrailer()
    {
        return _readyForParkingTrailer;
    }

    public void ManageParkingPlace()
    {
        var trailers = Game.instance.GetTrailerList();
        for(var i = 0; i<trailers.Count; i++)
        {
            trailers[i].GetComponent<TrailerParking>().SetCurve(parkingPlaces[trailers.Count-1 - i].GetCurve());
        }
    }
}
