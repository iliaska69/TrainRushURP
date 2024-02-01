using UnityEngine;
using UnityEngine.EventSystems;

public class TrailerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0f;
    [SerializeField] private float maxSpeed = 7f;
    [SerializeField] private float minSpeed = 0f;
    [SerializeField] private float speedAmplitude = 1f;
    [SerializeField] private float rotationSmooth = 0.05f;

    private bool _isReadyToGoing = false;
    private Vector3 _target;
    private Transform _transform;
    private Transform _nextTrailerTransform;
    private bool _isFinish;
    private bool _isMainTrailer;
    private Railway _railway;
    private Trailer _nextTrailer;
    private int _gameObjectId;

    private float _distanceBetweenTrailers;


    private void Start()
    {
        if(_isMainTrailer)
            _isReadyToGoing = true;

        speed = 0f;
        _transform = transform;
        _railway = GetComponentInParent<Railway>();
        _gameObjectId = gameObject.GetInstanceID();
        _target = _railway.GetNextPointToMove(_gameObjectId);
    }

    private void Update()
    {
        if (!_isReadyToGoing) return;
        if (Time.timeScale == 0f) return;

        SpeedControl();
        if (_isMainTrailer)
        {
            MainTrailerMovement();
        }
        else
        {
            NotMainTrailerMovement();
        }
        TrailerRotation();
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
        speed = newSpeed;
    }

    public void AddSpeed(float newSpeed)
    {
        speed += newSpeed;
    }

    public void SetIsMainTrailer(bool value)
    {
        if(value)
            _isReadyToGoing = value;

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
        float step;
        step = speed * Time.deltaTime;

        _transform.position = Vector3.MoveTowards(_transform.position, _target, step);

        if (Vector3.Distance(_transform.position, _target) < step)
        {
            _target = _railway.GetNextPointToMove(_gameObjectId);
        }
    }

    void NotMainTrailerMovement()
    {
        var step = speed * Time.deltaTime;

        _distanceBetweenTrailers = Vector3.Distance(_transform.position, _nextTrailerTransform.position);
        var distanceToStopPoint = _distanceBetweenTrailers - 2.5f;
        if (_nextTrailer == null ||
            _distanceBetweenTrailers < 2.8f)
        {
            //Debug.Log("Дистанция уменьшилась" + _distanceBetweenTrailers);
            return;
        }
        else
        {
            var speedMultiplicator = Mathf.Clamp(distanceToStopPoint, 1f, 1.5f);
            step = speed * speedMultiplicator * Time.deltaTime;
        }
        
        _transform.position = Vector3.MoveTowards(_transform.position, _target, step);

        if (Vector3.Distance(_transform.position, _target) < step)
        {
            _target = _railway.GetNextPointToMove(_gameObjectId);
        }
    }

    void TrailerRotation()
    {
        var rotation = _railway.GetRotation(_gameObjectId);
        if (rotation != Vector3.zero)
        {
            _transform.rotation = Quaternion.Lerp(
                _transform.rotation,
                Quaternion.LookRotation(rotation),
                rotationSmooth);
        }
    }

    void SpeedControl()
    {
        if (Input.anyKey || Input.touchCount > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            Acceleration();
        }
        else SlowDown();
    }

    void SlowDown()
    {
        if (speed <= minSpeed) return;
        speed -= speedAmplitude;
        speed = Mathf.Abs(speed);
    }

    void Acceleration()
    {
        if (speed > maxSpeed) return;
        speed += speedAmplitude;
    }
}
