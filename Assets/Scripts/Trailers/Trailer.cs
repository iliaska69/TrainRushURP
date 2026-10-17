using UnityEngine;

[RequireComponent(typeof(TrailerMovement))]
public class Trailer : MonoBehaviour, IDestroyable
{
    [Header("Animations")]
    [SerializeField] private CharacterAnimationController characterAnimationController;
    [SerializeField] private TrailerConnectAnimation trailerConnectAnimation;
    [SerializeField] private CrashTrailerAnim _crashTrailerAnim;
    [SerializeField] private EntryTrailerEngine _entryTrailerEngine;
    [SerializeField] private TrailerCollectEmojiVfx collectEmojiVfx;
    
    [Header("")]
    [SerializeField] private GameObject cameraTargetFollowingGameObject;
    [SerializeField] private GameObject catchGameObject;
    [SerializeField] private GameObject joinablePoint;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask joinableMask;
    [SerializeField] private bool isMainTrailer;
    [SerializeField] private int finishLayer;
    [SerializeField] private int catchingBonusLayer;

    [Header("")]
    [SerializeField] private TrailerEventChanelSO changedMainTrailerEventChanelSO;
    [SerializeField] private TrailerEventChanelSO incrementCountOfTrailersTrailerEvenChanelSO;
    [SerializeField] private TrailerEventChanelSO decrementCountOfTrailersTrailerEvenChanelSO;
    [SerializeField] private VoidEventChannelSO victoryVoidEvenChanelSO;
    [SerializeField] private VoidEventChannelSO collideFinishRailVoidEvenChanelSO;

    [Header("Sounds")] 
    [SerializeField] private SfxSound takeTrailersfxSound;
    [SerializeField] private SfxSound deathTrailersfxSound;
    
    private Trailer _nextTrailer;
    private Trailer _previousTrailer;
    private TrailerMovement _nextTrailerMovement;
    private TrailerMovement _myTrailerMovement;
    private TrailerParking _trailerParking;
    private bool _isReadyForParking;
    private bool _isFall;
    

    void Start()
    {
        _myTrailerMovement = gameObject.GetComponent<TrailerMovement>();
        _trailerParking = gameObject.GetComponent<TrailerParking>();
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
        if (_myTrailerMovement == null)
            _myTrailerMovement = gameObject.GetComponent<TrailerMovement>();

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
        if (_myTrailerMovement != null)
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

    public void SetNextTrailer(Trailer trailer)
    {
        _nextTrailer = trailer;

        _nextTrailerMovement = _nextTrailer.GetTrailerMovementComponent();
        if (_myTrailerMovement != null)
        {
            _myTrailerMovement.SetNextTrailer(trailer);
        }
        else
        {
            _myTrailerMovement = gameObject.GetComponent<TrailerMovement>();
            _myTrailerMovement.SetNextTrailer(trailer);
        }
    }

    public void DestroyWithAnimation(Transform obstacleTransform)
    {
        if (_isFall) return;
        _isFall = true;

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
        else if (_previousTrailer == null)
        {
            _nextTrailer.ClearPreviousTrailer();
        }

        ClearTrailerConnection();
        _myTrailerMovement.enabled = false;
        decrementCountOfTrailersTrailerEvenChanelSO.RaiseEvent(this);
        CloseEngineAnimation();
        
        _crashTrailerAnim.StartFalling(obstacleTransform);
        deathTrailersfxSound.Play();
        Game.instance.DecrementCountOfTrailers(this);
    }

    public void DestroyWithoutAnimation()
    {
        if (_isFall) return;
        _isFall = true;

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
        else if (_previousTrailer == null)
        {
            _nextTrailer.ClearPreviousTrailer();
        }

        ClearTrailerConnection();
        _myTrailerMovement.enabled = false;
        decrementCountOfTrailersTrailerEvenChanelSO.RaiseEvent(this);
        
        gameObject.SetActive(false);
        Game.instance.DecrementCountOfTrailers(this);
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

    public Vector3 GetPositionToJoin() => joinablePoint.transform.position;
    public Vector3 GetPositionToCatch() => catchGameObject.transform.position;

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
            if (!isMainTrailer) return;
            collideFinishRailVoidEvenChanelSO.RaiseEvent();
        }
    }

    //Заставляет двигаться следующий поезд
    void CollideWithTrailer()
    {
        if (!catchGameObject.activeInHierarchy)
        {
            return;
        }
        
        var hits = Physics.OverlapSphere(catchGameObject.transform.position, 1.5f, joinableMask);

        if (hits is null || hits.Length != 1) return;
        foreach (var hit in hits)
        {
            var trailer = hit.gameObject.GetComponentInParent<Trailer>();
            
            trailer.SetActiveJoinablePoint(false);
            _nextTrailer = trailer;
            _nextTrailer.GetCharacterAnim().SetIsAnimate(true);

            TrailerConnect(_nextTrailer);
        }
    }

    private void TrailerConnect(Trailer trailer)
    {
        if (trailer == this) return;
        if (isMainTrailer)
        {
            trailer.SetMainTrailer(true);
            trailer.GetTrailerMovementComponent().SetIsReadyToGoing(true);
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
        trailer.PunchAnimation();
        trailer.PlayCollectEmojiVfx();
        trailer.StartEntryEngineAnimation();
        trailer.StartEntryEngineAnimation();
        
        takeTrailersfxSound.Play();
        
        changedMainTrailerEventChanelSO.RaiseEvent(trailer);
        incrementCountOfTrailersTrailerEvenChanelSO.RaiseEvent(trailer);
    }

    public TrailerParking GetTrailerParkingComp() => _trailerParking;

    public bool IsReadyForParking() => _isReadyForParking;

    public void SetIsReadyForParking(bool value) => _isReadyForParking = value;

    public Transform GetCameraFollowingTarget() => cameraTargetFollowingGameObject.transform;

    public CharacterAnimationController GetCharacterAnim() => characterAnimationController;

    public CrashTrailerAnim GetCrashTrailerAnim() => _crashTrailerAnim;
    
    public void SetIsFall(bool value) => _isFall = value;

    public void PunchAnimation() => trailerConnectAnimation.BounceAnimation();

    public void PlayCollectEmojiVfx()
    {
        if (collectEmojiVfx == null)
            collectEmojiVfx = GetComponentInChildren<TrailerCollectEmojiVfx>(true);

        collectEmojiVfx?.Play();
    }

    public void StartEntryEngineAnimation() => _entryTrailerEngine.StartEntryAnimation();
    public void CloseEngineAnimation() => _entryTrailerEngine.CloseEngineAnimation();
}