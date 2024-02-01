using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleSevenAnimation : MonoBehaviour
{
    [SerializeField] private float rotationDuration;
    [SerializeField] private float delayBetweenAnimations;
    [SerializeField] private float startRotationZ;
    [SerializeField] private float targetRotationZ;

    private Transform _transform;

    void Start()
    {
        _transform = transform;
        RotateToTarget();
    }

    void RotateToTarget()
    {
        _transform.DORotate(new Vector3(0, 0, targetRotationZ), rotationDuration)
            .SetEase(Ease.InOutQuart)
            .OnComplete(() => Invoke("RotateBack", delayBetweenAnimations));
    }

    private void RotateBack()
    {
        _transform.DORotate(new Vector3(0, 0, startRotationZ), rotationDuration)
            .SetEase(Ease.InOutQuart)
            .OnComplete(() => Invoke("RotateToTarget", delayBetweenAnimations));
    }
}
