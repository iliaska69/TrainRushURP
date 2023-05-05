using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrailerMovement : MonoBehaviour
{
    [SerializeField] private BezierCurve startCurve;
    [SerializeField] private float startTParameter = 0f;
    [SerializeField] private float speed = 0f;
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float minSpeed = 0f;
    [SerializeField] private float speedAmplitude = 0.01f;
    [SerializeField] private bool isGoing;
    [SerializeField] private bool _isMainTrailer;

    private BezierCurve _currentCurve;
    private List<Transform> _points;
    private float _t;
    private Vector3 _target;
    private Vector3 _rotation;
    private float _distanceBetweenTrailers;
    private Vector3 _firstDistancePoint;
    private Vector3 _lastDistancePoint;
    private float _notMainTrailerSpeed = 5f;



    private void Start()
    {
        _t = startTParameter;
        _currentCurve = startCurve;
        speed = minSpeed;
        if (startCurve == null) return;
        _target = GetNextPointToMove(_currentCurve, _t);
        SetIsGoing(isGoing);
    }

    private void Update()
    {
        if (_isMainTrailer)
        {
            MainTrailerMovement();
        }
        else
        {
            NotMainTrailerMovement();
        }
    }

    public void SetIsGoing(bool set)
    {
        gameObject.GetComponent<TrailerMovement>().enabled = set;
    }

    public void SetStartPosition(BezierCurve curve, float tParam)
    {
        startCurve = curve;
        startTParameter = tParam;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
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
        transform.rotation = Quaternion.LookRotation(_rotation);
    }

    void MainTrailerMovement()
    {
        float step;
        step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, _target, step);

        if (Input.GetKey(KeyCode.F)) SlowDown();
        else Acceleration();

        if (Vector3.Distance(transform.position, _target) < 0.001f)
        {
            _t += 0.05f;
            _target = GetNextPointToMove(_currentCurve, _t);
            Rotation();
        }

        if (_t < 1f) return;

        _currentCurve = GetNextCurve();
        if (_currentCurve is null) return;

        _t = 0f;
        _target = GetNextPointToMove(_currentCurve, _t);
    }

    void NotMainTrailerMovement()
    {
        SetDistancesBetweenPoints(_firstDistancePoint, _lastDistancePoint);
        float step;
        step = _notMainTrailerSpeed * (_distanceBetweenTrailers - 0.6f) * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, _target, step);

        if (Vector3.Distance(transform.position, _target) < 0.001f)
        {
            _t += 0.05f;
            _target = GetNextPointToMove(_currentCurve, _t);
            Rotation();
        }
        
        if (_t < 1f) return;
        
        _currentCurve = GetNextCurve();
        if (_currentCurve is null) return;

        _t = 0f;
        _target = GetNextPointToMove(_currentCurve, _t);
    }

    private Vector3 GetNextPointToMove(BezierCurve curve, float t)
    {
        _points = GetCurvePoints(curve);
        return Bezier.GetPoint(_points[0], _points[1], _points[2], _points[3], t);
    }

    private List<Transform> GetCurvePoints(BezierCurve curve)
    {
        return curve.GetAllPoints();
    }

    private BezierCurve GetNextCurve()
    {
        var hitColliders = Physics.OverlapSphere(transform.position, 0.2f);
        foreach (var hit in hitColliders)
        {
            var rail = hit.gameObject.GetComponent<Rail>();
            if (rail is not null)
            {
                var nextCurve = rail.GetCurve();
                if (nextCurve != null && _currentCurve != nextCurve && rail.GetLicenseToMove())
                {
                    return nextCurve;
                }
            }
        }

        return null;
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
}
