using System.Collections.Generic;
using UnityEngine;

public class TrailerParking : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotationSmooth = 0.1f;

    private Transform _transform;
    private Vector3 _rotation;
    private Vector3 _target;
    private float _t;
    private BezierCurve _currentCurve;
    private List<Transform> _points;


    private void Start()
    {
        _transform = transform;
        _t = 0;
        _target = GetNextPointToMove(_currentCurve, _t);
    }

    private void Update()
    {
        float step;

        step = speed * Time.deltaTime;
        step = Mathf.Clamp(step, 0f, Vector3.Distance(_transform.position, _target));
        Rotation();
        _transform.position = Vector3.MoveTowards(_transform.position, _target, step);

        if (_t >= 1f) return;

        if (Vector3.Distance(_transform.position, _target) < 0.1f)
        {
            _t += 0.05f;
            if (_currentCurve == null) return;
            _target = GetNextPointToMove(_currentCurve, _t);
        }
    }

    public void SetCurve(BezierCurve curve)
    {
        _currentCurve = curve;
    }

    void Rotation()
    {
        _rotation = Bezier.GetFirstDerivative(_points[0], _points[1], _points[2], _points[3], _t);
        _transform.rotation = Quaternion.Lerp(_transform.rotation, Quaternion.LookRotation(_rotation), rotationSmooth);
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


}
