using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObstacleRotationZ : MonoBehaviour
{
    [SerializeField] private float rotationDuration = 2f;
    [SerializeField] private float delayBetweenAnimations = 1f;
    [SerializeField] private float startDelay;

    private bool isRotating;


    void Start()
    {
        Invoke("StartRotation", startDelay);
    }

    void StartRotation()
    {
        if (!isRotating)
        {
            isRotating = true;
            
            transform.DOLocalRotate(new Vector3(0f, 0f, 360f), rotationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutQuint)
                .OnComplete(() => OnRotationComplete());
        }
    }

    void OnRotationComplete()
    {
        isRotating = false;

        Invoke("StartRotation", delayBetweenAnimations);
    }
}
