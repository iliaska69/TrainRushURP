using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotationZ : MonoBehaviour
{
    [SerializeField] private float rotationDuration = 2f;
    [SerializeField] private float _delayBetweenAnimations;

    private bool isRotating;


    void Start()
    {
        Invoke("StartRotation", 1f);
    }

    void StartRotation()
    {
        if (!isRotating)
        {
            isRotating = true;
            
            transform.DORotate(new Vector3(0f, 0f, 360f), rotationDuration, RotateMode.FastBeyond360)
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
