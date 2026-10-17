using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveTrailerManager : MonoBehaviour
{
    [SerializeField] private TrailerEventChanelSO changedMainTrailerEventSO;
    [SerializeField] private TrailerEventChanelSO incrementCountOfTrailersTrailerEvenChanelSO;
    [SerializeField] private VoidEventChannelSO findCameraTargetEventChannelSo;

    //RAILWAY backup Dictionaries

    //Key: id trailer, value: Id curve
    private Dictionary<int, int> _dictionaryOfObjectsOnCurves = new();

    //Key: id trailer, value: Id point on curve
    private Dictionary<int, int> _dictionaryOfObjectsOnPoints = new();

    //Key: id trailer, value: his postion
    private Dictionary<int, Vector3> _dictionaryOfObjectsWithPositions = new();

    //Key: id trailer, value: his rotation
    private Dictionary<int, Quaternion> _dictionaryOfObjectsWithRotations = new();


    private List<Trailer> _savedTrailers = new();
    private Railway _railway;


    private void Start()
    {
        _railway = Game.instance.GetRailway();
    }

    public void Save()
    {
        ClearPrivateCollections();
        CopyFromRailwayToPrivateCollections();
    }

    public void ActivateSavedTrailers()
    {
        SetRailwayDictionaries();
        ImplementNextAndPreviousTrailer();

        for (var i = 0; i < _savedTrailers.Count; i++)
        {
            var trailer = _savedTrailers[i];
            trailer.SetMainTrailer(i == _savedTrailers.Count - 1);
            trailer.GetTrailerMovementComponent().GetNewNextPointToMove();
            trailer.GetTrailerMovementComponent().SetSpeed(0.1f);
            trailer.GetTrailerMovementComponent().enabled = true;
            trailer.SetIsFall(false);
            trailer.GetCrashTrailerAnim().EnableColliders(true);
            trailer.GetCrashTrailerAnim().ResetToInitialState();
            
            if (_dictionaryOfObjectsWithPositions.TryGetValue(trailer.gameObject.GetInstanceID(), out var trailerPosition))
            {
                trailer.transform.position = trailerPosition;
            }

            if (_dictionaryOfObjectsWithRotations.TryGetValue(trailer.gameObject.GetInstanceID(), out var trailerRotation))
            {
                trailer.transform.rotation = trailerRotation;
            }

            Game.instance.IncrementCountOfTrailers(trailer);

            trailer.gameObject.SetActive(true);
        }
        
        findCameraTargetEventChannelSo.RaiseEvent();
    }

    public void ImplementNextAndPreviousTrailer()
    {
        for (var i = 0; i < _savedTrailers.Count; i++)
        {
            if (i == 0)
            {
                if (_savedTrailers.Count == 1)
                    break;

                _savedTrailers[i].SetNextTrailer(_savedTrailers[i + 1]);
            }
            else if (i == _savedTrailers.Count - 1)
            {
                _savedTrailers[i].SetPreviousTrailer(_savedTrailers[i - 1]);
                _savedTrailers[i].SetMainTrailer(true);
            }
            else
            {
                _savedTrailers[i].SetNextTrailer(_savedTrailers[i + 1]);
                _savedTrailers[i].SetPreviousTrailer(_savedTrailers[i - 1]);
            }
        }
    }

    private void SetRailwayDictionaries()
    {
        foreach (var dictionaryElement in _dictionaryOfObjectsOnCurves)
        {
            _railway.SetDictionaryOfObjectOnCurves(dictionaryElement.Key, dictionaryElement.Value);
        }

        foreach (var dictionaryElement in _dictionaryOfObjectsOnPoints)
        {
            _railway.SetDictionaryOfObjectOnPoints(dictionaryElement.Key, dictionaryElement.Value);
        }
    }

    private void CopyFromRailwayToPrivateCollections()
    {
        var dictionaryOfObjectsOnCurves = _railway.GetDictionaryOfObjectOnCurves();
        var dictionaryOfObjectsOnPoints = _railway.GetDictionaryOfObjectOnPoints();

        foreach (var dictionaryElement in dictionaryOfObjectsOnCurves)
        {
            SetDictionaryOfObjectOnCurves(dictionaryElement.Key, dictionaryElement.Value);
        }

        foreach (var dictionaryElement in dictionaryOfObjectsOnPoints)
        {
            SetDictionaryOfObjectOnPoints(dictionaryElement.Key, dictionaryElement.Value);
        }

        foreach (var trailer in Game.instance.GetTrailerList())
        {
            _savedTrailers.Add(trailer);
        }

        ImplementNextAndPreviousTrailer();

        SaveTrailersPosition();
        SaveTrailersRotation();
    }

    private void SaveTrailersPosition()
    {
        foreach (var trailer in _savedTrailers)
        {
            _dictionaryOfObjectsWithPositions.Add(trailer.gameObject.GetInstanceID(), trailer.transform.position);
        }
    }

    private void SaveTrailersRotation()
    {
        foreach (var trailer in _savedTrailers)
        {
            _dictionaryOfObjectsWithRotations.Add(trailer.gameObject.GetInstanceID(), trailer.transform.rotation);
        }
    }

    private void SetDictionaryOfObjectOnCurves(int key, int value)
    {
        if (_dictionaryOfObjectsOnCurves.ContainsKey(key))
            _dictionaryOfObjectsOnCurves[key] = value;
        else
            _dictionaryOfObjectsOnCurves.Add(key, value);
    }

    private void SetDictionaryOfObjectOnPoints(int key, int value)
    {
        if (_dictionaryOfObjectsOnPoints.ContainsKey(key))
            _dictionaryOfObjectsOnPoints[key] = value;
        else
            _dictionaryOfObjectsOnPoints.Add(key, value);
    }

    private void ClearPrivateCollections()
    {
        _savedTrailers.Clear();
        _dictionaryOfObjectsOnCurves.Clear();
        _dictionaryOfObjectsOnPoints.Clear();
        _dictionaryOfObjectsWithPositions.Clear();
        _dictionaryOfObjectsWithRotations.Clear();
    }
}