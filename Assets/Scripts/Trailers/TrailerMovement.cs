using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrailerMovement : MonoBehaviour
{
    [Range(0, 1)][SerializeField] private float startTParameter = 0f;
    [SerializeField] private BezierCurve startCurve;
    [SerializeField] private float speed = 0f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float minSpeed = 0f;
    [SerializeField] private float speedAmplitude = 0.5f;
    [SerializeField] private bool isReadyToGoing;
    [SerializeField] private float rotationSmooth = 0.1f;

    private BezierCurve _currentCurve;
    private Rail _currentRail;
    private List<BezierCurve> _railCurves;
    private List<Transform> _points;
    private float _t;
    private Vector3 _target;
    private Vector3 _rotation;
    private float _distanceBetweenTrailers;
    private Vector3 _firstDistancePoint;
    private Vector3 _lastDistancePoint;
    private float _notMainTrailerSpeed = 5f;
    private Transform _transform;
    private bool _isFinish;
    private bool _isMainTrailer;
    private int _curveIndex;


    private void Start()
    {
        _transform = transform;
        _t = startTParameter;
        _currentCurve = startCurve;
        speed = minSpeed;
        SetSpeedTakingTrailer();
        if (startCurve == null) return;
        if (_currentCurve == null) return;
        _target = GetNextPointToMove(_currentCurve, _t);
        _isMainTrailer = gameObject.GetComponent<Trailer>().IsMainTrailer();
        GetRailCurves();
    }

    private void Update()
    {
        if (!isReadyToGoing) return;
        if (Time.timeScale == 0f) return;

        if (_isMainTrailer)
        {
            MainTrailerMovement();
        }
        else
        {
            NotMainTrailerMovement();
        }
    }

    public void SetIsFinish(bool set)
    {
        _isFinish = set;
    }

    public void SetIsGoing(bool set)
    {
        isReadyToGoing = set;
    }

    public void SetStartPosition(Rail startRail, BezierCurve curve, float tParam)
    {
        startCurve = curve;
        startTParameter = tParam;
        _currentRail = startRail;
    }

    public BezierCurve GetBezierCurve()
    {
        return _currentCurve;
    }

    public float GetTParametr()
    {
        return _t;
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

    public void SetDistancesPoints(Vector3 firstpoint, Vector3 secondPoint)
    {
        _firstDistancePoint = firstpoint;
        _lastDistancePoint = secondPoint;
    }

    public void SetIsMainTrailer(bool set)
    {
        _isMainTrailer = set;
    }

    public bool GetIsMainTrailer()
    {
        return _isMainTrailer;
    }

    float GetDistanceBetweenPoints()
    {
        return _distanceBetweenTrailers;
    }

    void SetDistancesBetweenPoints(Vector3 firstpoint, Vector3 secondPoint)
    {
        _distanceBetweenTrailers = Vector3.Distance(firstpoint, secondPoint);
    }

    void Rotation()
    {
        _rotation = Bezier.GetFirstDerivative(_points[0], _points[1], _points[2], _points[3], _t);
        _transform.rotation = Quaternion.Lerp(_transform.rotation, Quaternion.LookRotation(_rotation), rotationSmooth);
    }

    void MainTrailerMovement()
    {
        float step;
        if (_isFinish)
        {
            speed = 10f;
        }

        step = speed * Time.deltaTime;
        step = Mathf.Clamp(step, 0f, Vector3.Distance(_transform.position, _target));

        Rotation();

        _transform.position = Vector3.MoveTowards(_transform.position, _target, step);

        if (Input.anyKey || Input.touchCount > 0) Acceleration();
        else SlowDown();

        if (Vector3.Distance(_transform.position, _target) < 0.1f)
        {
            _t += 0.05f;
            if (_currentCurve == null) return;
            _target = GetNextPointToMove(_currentCurve, _t);
        }

        if (_t < 1f) return;
        
        _currentCurve = GetNextCurve();
        
        if (_currentCurve == null && !_isFinish)
        {
            var trailer = GetComponent<Trailer>();
            trailer.DestroyWithAnimation();
            return;
        }

        _t = 0f;
        if (_currentCurve is null) return;
        _target = GetNextPointToMove(_currentCurve, _t);
    }

    void NotMainTrailerMovement()
    {
        SetDistancesBetweenPoints(_firstDistancePoint, _lastDistancePoint);
        float step;
        step = _notMainTrailerSpeed * (_distanceBetweenTrailers - 0.6f) * Time.deltaTime;
        step = Mathf.Clamp(step, 0, _distanceBetweenTrailers - 0.6f);
        if (step < 0) return;

        Rotation();
        _transform.position = Vector3.MoveTowards(_transform.position, _target, step);

        if (Vector3.Distance(_transform.position, _target) < 0.1f)
        {
            _t += 0.1f;
            if (_currentCurve == null) return;
            _target = GetNextPointToMove(_currentCurve, _t);

        }

        if (_t < 1f) return;

        _currentCurve = GetNextCurve();
        if (_currentCurve == null && !_isFinish)
        {
            var trailer = GetComponent<Trailer>();
            trailer.DestroyWithAnimation();
            return;
        }

        _t = 0f;
        if (_currentCurve is null) return;
        _target = GetNextPointToMove(_currentCurve, _t);
    }

    private Vector3 GetNextPointToMove(BezierCurve curve, float t)
    {
        _points = GetCurvePoints(curve);
        return Bezier.GetPoint(_points[0], _points[1], _points[2], _points[3], t);
    }

    private List<Transform> GetCurvePoints(BezierCurve curve)
    {
        if (curve == null) return null;
        var points = curve.GetAllPoints();
        return points;
    }

    private Rail GetNextRail()
    {
        var hitColliders = Physics.OverlapSphere(_transform.position, 0.2f);

        foreach (var hit in hitColliders)
        {
            var rail = hit.gameObject.GetComponent<Rail>();

            if (rail == null) continue;
            _railCurves = rail.GetCurves();
            _curveIndex = 0;
            
            return rail;
        }

        return null;
    }

    private BezierCurve GetNextCurve()
    {
        _curveIndex++;
        if(_curveIndex == _railCurves.Count)
        {
            _currentRail = GetNextRail();
        }
        return _railCurves[_curveIndex];
    }

    private void GetRailCurves()
    {
        _railCurves = _currentRail.GetCurves();
        for (var i = 0; i < _railCurves.Count; i++)
        {
            if (_currentCurve == _railCurves[i])
                _curveIndex = i;
        }
    }

    private void SlowDown()
    {
        if (speed <= minSpeed) return;
        speed -= speedAmplitude;
        speed = Mathf.Abs(speed);
    }

    private void Acceleration()
    {
        if (speed > maxSpeed) return;
        speed += speedAmplitude;
    }

    private void SetSpeedTakingTrailer()
    {
        var shopItem = Shop.instance.GetCurrentShopItem();
        var itemSpeed = shopItem.GetSpeed();
        maxSpeed = itemSpeed;
        _notMainTrailerSpeed += itemSpeed;
    }
}
