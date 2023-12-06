using System;
using System.Net;
using TMPro;
using UnityEngine;

public class Trailer : MonoBehaviour, IDestroyable
{
    [SerializeField] private GameObject catchGameObject;
    [SerializeField] private GameObject joinablePoint;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask joinableMask;
    [SerializeField] private bool isMainTrailer;
    [SerializeField] private int finishLayer;
    [SerializeField] private int catchingBonusLayer;

    [SerializeField] private TrailerEventChanelSO changedMainTrailerEventChanelSO;
    [SerializeField] private TrailerEventChanelSO incrementCountOfTrailersTrailerEvenChanelSO;
    [SerializeField] private TrailerEventChanelSO decrementCountOfTrailersTrailerEvenChanelSO;
    [SerializeField] private VoidEventChannelSO victoryVoidEvenChanelSO;
    [SerializeField] private VoidEventChannelSO finishVoidEvenChanelSO;

    private Trailer _nextTrailer;
    private Trailer _previousTrailer;
    private TrailerMovement _nextTrailerMovement;
    private TrailerMovement _myTrailerMovement;


    void Start()
    {
        _myTrailerMovement = gameObject.GetComponent<TrailerMovement>();
        _myTrailerMovement.SetIsMainTrailer(isMainTrailer);
        if (isMainTrailer)
        {
            incrementCountOfTrailersTrailerEvenChanelSO.RaiseEvent(this);
        }
    }

    private void Update()
    {
        if (Time.timeScale == 0f) return;

        CollideWithTrailer();
    }

    public TrailerMovement GetTrailerMovementComponent()
    {
        if(_myTrailerMovement == null)
            return null;

        return _myTrailerMovement;
    }

    public void SetActiveJoinablePoint(bool set)
    {
        joinablePoint.SetActive(set);
    }

    public bool IsMainTrailer()
    {
        return isMainTrailer;
    }

    public void SetMainTrailer(bool value)
    {
        isMainTrailer = value;
        if(_myTrailerMovement != null)
            _myTrailerMovement.SetIsMainTrailer(value);
        if (value)
            _nextTrailer = null;
        changedMainTrailerEventChanelSO.RaiseEvent(this);
    }

    public void SetPreviousTrailer(Trailer prevTrailer)
    {
        _previousTrailer = prevTrailer;
    }

    public Trailer GetPreviousTrailer()
    {
        return _previousTrailer;
    }

    public void SetNextTrailer(Trailer trialer)
    {
        _nextTrailer = trialer;
        _nextTrailerMovement = _nextTrailer.GetTrailerMovementComponent();
        if (_myTrailerMovement != null)
        {
            _myTrailerMovement.SetNextTrailer(trialer);
        }
        else
        {
            _myTrailerMovement = gameObject.GetComponent<TrailerMovement>();
            _myTrailerMovement.SetNextTrailer(trialer);
        }
    }

    public void DestroyWithAnimation()
    {
        if (isMainTrailer)
        {
            SetMainTrailer(false);
            if (_previousTrailer != null)
            {
                _previousTrailer.SetMainTrailer(true);
                _previousTrailer.ClearNextTrailer();
                changedMainTrailerEventChanelSO.RaiseEvent(_previousTrailer);
            }
        }
        else if (_nextTrailer != null && _previousTrailer != null)
        {
            _nextTrailer.SetPreviousTrailer(_previousTrailer);
            _previousTrailer.SetNextTrailer(_nextTrailer);
        }
        else if(_previousTrailer == null)
        {
            _nextTrailer.ClearPreviousTrailer();
        }

        ClearTrailerConnection();
        _myTrailerMovement.enabled = false;
        decrementCountOfTrailersTrailerEvenChanelSO.RaiseEvent(this);
        gameObject.SetActive(false);

        //boxCollider.enabled = false;
        //animator.SetTrigger("Death");
    }

    public void ClearNextTrailer()
    {
        _nextTrailer = null;
        _myTrailerMovement.ClearNextTrailer();
        _nextTrailerMovement = null;
    }

    public void ClearPreviousTrailer()
    {
        _previousTrailer = null;
    }

    private void ClearTrailerConnection()
    {
        _previousTrailer = null;
        _nextTrailer = null;
        _nextTrailerMovement = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == catchingBonusLayer)
        {
            other.GetComponent<IActivatable>()?.ActivateBonus();
        }
        if (other.gameObject.layer == finishLayer)
        {
            _myTrailerMovement.SetIsFinish(true);
            if (!isMainTrailer) return;
            finishVoidEvenChanelSO.RaiseEvent();
        }
    }

    //«аставл€ет двигатьс€ следующий поезд
    void CollideWithTrailer()
    {
        var hits = Physics.OverlapSphere(catchGameObject.transform.position, 1.5f, joinableMask);
        if (hits is null || hits.Length != 1) return;
        foreach (var hit in hits)
        {
            var trailer = hit.gameObject.GetComponentInParent<Trailer>();
            trailer.SetActiveJoinablePoint(false);
            _nextTrailer = trailer;

            TrailerConnect(_nextTrailer);
        }
    }

    private void TrailerConnect(Trailer trailer)
    {
        if (trailer == this) return;
        if (isMainTrailer)
        {
            trailer.SetMainTrailer(true);
            SetMainTrailer(false);
            trailer.SetPreviousTrailer(this);
            SetNextTrailer(trailer);
        }
        _nextTrailerMovement = trailer.gameObject.GetComponent<TrailerMovement>();

        var mySpeed = _myTrailerMovement.GetSpeed();
        _nextTrailerMovement.SetSpeed(mySpeed);

        _myTrailerMovement.SetIsMainTrailer(false);
        _nextTrailerMovement.SetIsMainTrailer(true);
        _myTrailerMovement.SetNextTrailer(trailer);

        changedMainTrailerEventChanelSO.RaiseEvent(trailer);
        incrementCountOfTrailersTrailerEvenChanelSO.RaiseEvent(trailer);
    }
}
