using UnityEngine;
using UnityEngine.EventSystems;

public class TrailerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0f;
    [SerializeField] private float maxSpeed = 7f;
    [SerializeField] private float finishSpeed = 26f;
    [SerializeField] private float minSpeed = 0f;
    [SerializeField] private float speedAmplitude = 1f;
    [SerializeField] private float rotationSmooth = 0.05f;
    [SerializeField] private bool _isReadyToGoing;

    private CharacterAnimationController _characterAnimationController;
    private BezierPointInfo _target;
    private Transform _transform;
    private Transform _nextTrailerTransform;
    private bool _isFinish;
    private bool _isMainTrailer;
    private Railway _railway;
    private Trailer _nextTrailer;
    private Trailer _currentTrailer;
    private int _gameObjectId;

    private float _distanceBetweenTrailers;
    private float _maxSpeedBeforeBonus;
    private float _speedBeforeBonus;
    private bool _isBonusSpeedActive;


    private void Start()
    {
        speed = 0f;
        _transform = transform;
        _currentTrailer = GetComponent<Trailer>();
        _railway = GetComponentInParent<Railway>();
        _gameObjectId = gameObject.GetInstanceID();
        _target = _railway.GetNextPointToMove(_gameObjectId);
        _characterAnimationController = _currentTrailer.GetCharacterAnim();
    }

    private void Update()
    {
        if (!_isReadyToGoing) return;
        if (Time.timeScale == 0f) return;
        if (_isFinish)
            MovementWithoutTouch();
        else
            MovementWithTouch();
    }

    public void SetMainTrailerIsReady(bool value)
    {
        if (!_isMainTrailer)
            return;

        _isReadyToGoing = value;
    }

    public void SetNextTrailer(Trailer trailer)
    {
        _nextTrailer = trailer;
        _nextTrailerTransform = _nextTrailer.transform;
    }

    public void GetNewNextPointToMove()
    {
        _target = _railway.GetNextPointToMove(_gameObjectId);
    }

    public void SetIsFinish(bool set)
    {
        _isFinish = set;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float newSpeed)
    {
        if (newSpeed > maxSpeed)
            return;

        speed = maxSpeed;
    }

    public void SetBonusSpeed()
    {
        if (!_isBonusSpeedActive)
        {
            _maxSpeedBeforeBonus = maxSpeed;
            _speedBeforeBonus = speed;
            _isBonusSpeedActive = true;
        }

        maxSpeed = 35;
        speed = maxSpeed;
    }

    public void ResetBonusSpeed()
    {
        if (!_isBonusSpeedActive)
            return;

        maxSpeed = _maxSpeedBeforeBonus;
        speed = _speedBeforeBonus;
        _isBonusSpeedActive = false;
    }
    
    public void AddSpeed(float newSpeed)
    {
        if (newSpeed > maxSpeed)
            return;

        speed += newSpeed;
    }

    public void SetIsMainTrailer(bool value)
    {
        _isMainTrailer = value;
    }

    public void SetIsReadyToGoing(bool value)
    {
        _isReadyToGoing = value;
    }

    public void ClearNextTrailer()
    {
        _nextTrailer = null;
        _nextTrailerTransform = null;
    }

    void MainTrailerMovement()
    {
        var step = speed * Time.deltaTime;
        var direction = _target.position - _transform.position;

        if (_target.position == Vector3.zero)
            return;

        _transform.position = Vector3.MoveTowards(_transform.position, _target.position, step);
        var distance = Vector3.Distance(_transform.position, _target.position);
        if (distance < step)
        {
            _target = _railway.GetNextPointToMove(_gameObjectId);
            if (_target.position == Vector3.zero)
                return;
            direction = _target.position - _transform.position;

            SliderLevelBar.instance.SetSliderPercentByObjectId(_gameObjectId);
        }

        //Debug.Log("Main step: " + step);
        TrailerRotation(direction);
    }

    void NotMainTrailerMovement()
    {
        var step = speed * Time.deltaTime;
        var direction = _target.position - _transform.position;

        _distanceBetweenTrailers = Vector3.Distance(_transform.position, _nextTrailerTransform.position);
        var distanceToStopPoint = _distanceBetweenTrailers - 2.5f;

        if (_nextTrailer == null || _distanceBetweenTrailers < 2.6f)
        {
            return;
        }

        var speedMultiplicator = Mathf.Clamp(distanceToStopPoint, 1f, 2f);
        step = speed * speedMultiplicator * Time.deltaTime;

        _transform.position = Vector3.MoveTowards(_transform.position, _target.position, step);

        if (Vector3.Distance(_transform.position, _target.position) < step)
        {
            _target = _railway.GetNextPointToMove(_gameObjectId);

            direction = _target.position - _transform.position;
        }

        TrailerRotation(direction);
    }

    void MainTrailerMovementWithoutTouch()
    {
        var step = finishSpeed * Time.deltaTime;
        var direction = _target.position - _transform.position;

        if (_target.position == Vector3.zero)
            return;

        _transform.position = Vector3.MoveTowards(_transform.position, _target.position, step);

        if (Vector3.Distance(_transform.position, _target.position) < step)
        {
            _target = _railway.GetNextPointToMove(_gameObjectId);
            if (_target.position == Vector3.zero)
                return;

            direction = _target.position - _transform.position;
        }

        TrailerRotation(direction);
    }

    void NotMainTrailerMovementWithoutTouch()
    {
        var step = finishSpeed * Time.deltaTime;
        var direction = _target.position - _transform.position;

        _distanceBetweenTrailers = Vector3.Distance(_transform.position, _nextTrailerTransform.position);
        var distanceToStopPoint = _distanceBetweenTrailers - 3f;

        if (_nextTrailer == null || _distanceBetweenTrailers < 3.2f)
            return;

        var speedMultiplicator = Mathf.Clamp(distanceToStopPoint, 1f, 1.5f);
        step = finishSpeed * speedMultiplicator * Time.deltaTime;

        _transform.position = Vector3.MoveTowards(_transform.position, _target.position, step);

        if (Vector3.Distance(_transform.position, _target.position) < step)
        {
            _target = _railway.GetNextPointToMove(_gameObjectId);
            direction = _target.position - _transform.position;
        }

        TrailerRotation(direction);
    }

    void TrailerRotation(Vector3 direction)
    {
        if (direction == Vector3.zero) return;
        var targetRotation = Quaternion.LookRotation(direction, _target.normal);
        _transform.rotation = Quaternion.Lerp(
            _transform.rotation,
            targetRotation,
            rotationSmooth * Time.deltaTime
        );
    }

    void MovementWithTouch()
    {
         if (Input.anyKey || Input.touchCount > 0)
         {
             if (_isMainTrailer)
             {
                 MainTrailerMovement();
                 //MainTrailerAcceleration();
             }
             else
             {
                 NotMainTrailerMovement();
             }
        
             Acceleration();
             _characterAnimationController.SetIsAnimate(true);
         }
         else
         {
             SlowDown();
             _characterAnimationController.SetIsAnimate(false);
         }
         
    }

    void MovementWithoutTouch()
    {
        if (_isMainTrailer)
        {
            MainTrailerMovementWithoutTouch();
        }
        else
        {
            NotMainTrailerMovementWithoutTouch();
        }
        _characterAnimationController.SetIsAnimate(true);
    }

    void SlowDown()
    {
        if (speed <= minSpeed) return;
        speed -= speedAmplitude;
        speed = Mathf.Abs(speed);
    }

    void Acceleration()
    {
        if (speed >= maxSpeed) return;
        speed = Mathf.Clamp(speed+speedAmplitude,minSpeed, maxSpeed);
    }
}