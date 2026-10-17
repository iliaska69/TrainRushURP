using UnityEngine;

public class TrailerParking : MonoBehaviour
{
    [SerializeField] private float speed = 0f;
    [SerializeField] private float rotationSmooth = 0.05f;
    [SerializeField] private CharacterAnimationController _characterControllerAnim;
    
    private ParkingPlace _parkingPlace;
    private Transform _transform;
    private BezierPointInfo _target;
    private bool _isStop;
    private bool _isRealStop = true;

    
    private void Start()
    {
        _transform = transform;
        _target = _parkingPlace.GetNextPointToMove();
    }

    public void SetIsStop(bool value)
    { 
        _isStop = value;
        _isRealStop = value;
    }
    
    private void Update()
    {
        Parking();
    }

    public void SetParkingPlace(ParkingPlace parkingPlace)
    {
        _parkingPlace = parkingPlace;
    }
    
    private void Parking()
    {
        if (!_isStop)
        {
            _characterControllerAnim.PutHands(false);
            _characterControllerAnim.SetIsAnimate(true);
        }
        else
        {
            _characterControllerAnim.PutHands(true);
        }

        var step = speed * Time.deltaTime;
        var direction = _target.position - _transform.position;
        
        if(_target.position == Vector3.zero)
            return;
        
        _transform.position = Vector3.MoveTowards(_transform.position, _target.position, step);

        if (Vector3.Distance(_transform.position, _target.position) < step)
        {
            _target = _parkingPlace.GetNextPointToMove();
            
            if (_target.position == Vector3.zero)
            {
                _isStop = _isRealStop;
                _characterControllerAnim.PutHands(_isStop);
                return;
            }
                
            direction = _target.position - _transform.position;
        }
        
        TrailerRotation(direction);
    }
    
    private void TrailerRotation(Vector3 direction)
    {
        if (direction == Vector3.zero) return;
        var targetRotation = Quaternion.LookRotation(direction, _target.normal);
        _transform.rotation = Quaternion.Lerp(_transform.rotation, targetRotation, rotationSmooth);
    }
}
