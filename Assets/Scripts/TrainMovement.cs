using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    [SerializeField] GameObject startRail;
    [SerializeField] float speedAmplitude = 0.01f;
    [SerializeField] float speedMax;
    [SerializeField] float slowDownAmplitude = 0.5f;

    private IBezierCurve _curve;
    private float _trainPosition;
    private Transform _currentTransform;
    private Rail _currentRail;
    private float _currentSpeed;


    private void Start()
    {
        _currentRail = startRail.GetComponent<Rail>();
        _curve = _currentRail.GetComponentInChildren<IBezierCurve>();
        _currentTransform = transform;
        _currentSpeed = speedAmplitude;
    }

    private void Update()
    {
        if (_curve == null) return;
        Movement();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.F))
        {
            SlowDown();
        }
        Acceleration();
    }

    private void Movement()
    {
        var points = _curve.GetAllPoints();
        _currentTransform.position = Bezier.GetPoint(points[0], points[1], points[2], points[3], _trainPosition);
        _trainPosition += Time.deltaTime * _currentSpeed;
        

        if (_trainPosition >= 1f)
        {
            MoveToTheNextRail();
        }
    }

    private void MoveToTheNextRail()
    {
        var hitColliders = Physics.OverlapSphere(transform.position, 0.2f);
        foreach (var hit in hitColliders)
        {
            var rail = hit.gameObject.GetComponent<Rail>();
            if (rail is not null)
            {
                var nextCurve = rail.GetCurve();
                if (nextCurve != null && _curve != nextCurve && rail.GetLicenseToMove())
                {
                    _curve = nextCurve;
                    _trainPosition = 0f;

                    return;
                }
            }
        }
    }

    private void SlowDown()
    {
        Debug.Log(_currentSpeed);

        _currentSpeed -= slowDownAmplitude;
    }

    private void Acceleration()
    {
        if (_currentSpeed >= speedMax) return;

        _currentSpeed += speedAmplitude;
    }
}
