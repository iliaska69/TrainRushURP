using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSevenAnimation : MonoBehaviour
{
    [SerializeField] private float rotationDuration;
    [SerializeField] private float delayBetweenAnimations;
    [SerializeField] private float startRotationZ;
    [SerializeField] private float targetRotationZ;


    void Start()
    {
        RotateToTarget();
    }

    void RotateToTarget()
    {
        transform.DORotate(new Vector3(0, 0, targetRotationZ), rotationDuration)
            .SetEase(Ease.InOutQuart)
            .OnComplete(() => Invoke("RotateBack", delayBetweenAnimations));
    }

    private void RotateBack()
    {
        transform.DORotate(new Vector3(0, 0, startRotationZ), rotationDuration)
            .SetEase(Ease.InOutQuart)
            .OnComplete(() => Invoke("RotateToTarget", delayBetweenAnimations));
    }
}
