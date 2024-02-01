using DG.Tweening;
using UnityEngine;

public class ObstacleRotationZ : MonoBehaviour
{
    [SerializeField] private float rotationDuration = 2f;
    [SerializeField] private float _delayBetweenAnimations;

    private bool isRotating;
    private Transform _transform;


    void Start()
    {
        _transform = transform;
        Invoke("StartRotation", 1f);
    }

    void StartRotation()
    {
        if (!isRotating)
        {
            isRotating = true;

            _transform.DORotate(new Vector3(0f, 0f, 360f), rotationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutQuint)
                .OnComplete(() => OnRotationComplete());
        }
    }

    void OnRotationComplete()
    {
        isRotating = false;

        Invoke("StartRotation", 2f);
    }
}
