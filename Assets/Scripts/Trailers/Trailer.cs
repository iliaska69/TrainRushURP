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
    [SerializeField] private VoidEventChannelSO incrementVoidEvenChanelSO;
    [SerializeField] private VoidEventChannelSO decrementVoidEvenChanelSO;
    [SerializeField] private VoidEventChannelSO finishVoidEvenChanelSO;

    private Trailer _nextTrailer;
    private Trailer _previousTrailer;
    private TrailerMovement _nextTrailerMovement;
    private TrailerMovement _myTrailerMovement;


    void Start()
    {
        _myTrailerMovement = gameObject.GetComponent<TrailerMovement>();
    }
    private void Update()
    {
        if (_myTrailerMovement != null && _nextTrailer != null)
        {
            _myTrailerMovement.SetDistancesPoints(
                transform.position, _nextTrailer.transform.position);
        }

        var hits = Physics.OverlapSphere(catchGameObject.transform.position, 0.1f, joinableMask);
        if (hits is null || hits.Length != 1) return;
        foreach (var hit in hits)
        {
            var trailer = hit.gameObject.GetComponentInParent<Trailer>();
            trailer.SetActiveJoinablePoint(false);
            _nextTrailer = trailer;

            CollideWithTrailer(_nextTrailer);
        }
    }

    public void SetActiveJoinablePoint(bool set)
    {
        joinablePoint.SetActive(set);
    }

    public GameObject GetJoinablePoint()
    {
        return joinablePoint;
    }

    public bool IsMainTrailer()
    {
        return isMainTrailer;
    }

    public void SetMainTrailer(bool set)
    {
        isMainTrailer = set;
        _myTrailerMovement.SetIsMainTrailer(set);
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
    }

    public Trailer GetNextTrailer()
    {
        return _nextTrailer;
    }

    //«аставл€ет двигатьс€ следующий поезд
    private void CollideWithTrailer(Trailer trailer)
    {
        if (isMainTrailer)
        {
            trailer.SetMainTrailer(true);
            SetMainTrailer(false);
            trailer.SetPreviousTrailer(this);
            SetNextTrailer(trailer);
        }
        _nextTrailerMovement = trailer.gameObject.GetComponent<TrailerMovement>();
        _nextTrailerMovement.SetIsGoing(true);

        var mySpeed = _myTrailerMovement.GetSpeed();
        _nextTrailerMovement.SetSpeed(mySpeed);

        _myTrailerMovement.SetIsMainTrailer(false);
        _nextTrailerMovement.SetIsMainTrailer(true);

        changedMainTrailerEventChanelSO.RaiseEvent(trailer);
        incrementVoidEvenChanelSO.RaiseEvent();
    }

    public void Destroy()
    {
        if (isMainTrailer)
        {
            if (_previousTrailer != null)
            {
                _previousTrailer.SetMainTrailer(true);
                changedMainTrailerEventChanelSO.RaiseEvent(_previousTrailer);
            }
        }
        if (_nextTrailer != null)
        {
            _nextTrailer.SetPreviousTrailer(_previousTrailer);
        }
        if (_previousTrailer != null)
        {
            _previousTrailer.SetNextTrailer(_nextTrailer);
        }

        _myTrailerMovement.enabled = false;
        boxCollider.enabled = false;
        animator.SetTrigger("Death");
        decrementVoidEvenChanelSO.RaiseEvent();

        Destroy(gameObject, 3);
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
            finishVoidEvenChanelSO.RaiseEvent();
        }
    }
}
