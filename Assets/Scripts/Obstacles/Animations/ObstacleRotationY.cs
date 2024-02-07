using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleRotationY : MonoBehaviour
{
    [SerializeField] private float rotationDuration = 2f;
    [SerializeField] private float _delayBetweenAnimations;

    private bool _isRotating;
    private Transform _transform;

    void Start()
    {
        _transform = transform;
        Invoke("StartRotation", 2f);
    }

    void StartRotation()
    {
        if (!_isRotating)
        {
            _isRotating = true;

            // Ќачать вращение с учетом начального угла
            _transform.DORotate(new Vector3(0f, 360f, 0f), rotationDuration, RotateMode.WorldAxisAdd)
                .SetEase(Ease.Linear)
                .OnKill(() => OnRotationComplete());
        }
    }

    void OnRotationComplete()
    {
        _isRotating = false;

        Invoke("StartRotation", 2f);
    }
}
