using NaughtyAttributes;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Trailer : MonoBehaviour
{
    [SerializeField] private GameObject catchGameObject;
    [SerializeField] private GameObject joinablePoint;
    [SerializeField] private LayerMask joinableMask;
    [SerializeField] private bool isMainTrailer;

    private Trailer _nextTrailer;
    private Trailer _previousTrailer;
    private TrailerMovement _nextTrailerMovement;
    private TrailerMovement _myTrailerMovement;


    private void Update()
    {
        if (_myTrailerMovement != null && _nextTrailer != null)
        {
            _myTrailerMovement.SetDistancesPoints(
            catchGameObject.transform.position,
                    _nextTrailer.GetJoinablePoint().transform.position);
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
        
        _myTrailerMovement = gameObject.GetComponent<TrailerMovement>();
        
        var mySpeed = _myTrailerMovement.GetSpeed();
        _nextTrailerMovement.SetSpeed(mySpeed);

        _myTrailerMovement.SetIsMainTrailer(false);
        _nextTrailerMovement.SetIsMainTrailer(true);
    }

    public void CrushTrailer()
    {

        Destroy(gameObject);
    }

    void GetNewNextTrailer()
    {

    }
}
