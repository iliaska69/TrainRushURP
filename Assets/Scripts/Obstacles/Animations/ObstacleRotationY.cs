using DG.Tweening;
using UnityEngine;

public class ObstacleRotationY : MonoBehaviour
{
    [SerializeField] private float rotationDuration = 2f;
    [SerializeField] private float _delayBetweenAnimations;
    [SerializeField] private float _startDelay;

    private bool isRotating;


    void Start()
    {
        Invoke(nameof(StartRotation), _startDelay);
    }

    void StartRotation()
    {
        if (!isRotating)
        {
            isRotating = true;

            // ������ �������� � ������ ���������� ����
            transform.DORotate(new Vector3(0f, 360f, 0f), rotationDuration, RotateMode.WorldAxisAdd)
                .SetEase(Ease.Linear)
                .OnKill(() => OnRotationComplete());
        }
    }

    void OnRotationComplete()
    {
        isRotating = false;

        Invoke("StartRotation", 2f);
    }
}
