using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotationY : MonoBehaviour
{
    [SerializeField] private float rotationDuration = 2f;
    [SerializeField] private float _delayBetweenAnimations;

    private bool isRotating;
    private float initialRotationY;


    void Start()
    {
        StartRotation();
    }

    void StartRotation()
    {
        if (!isRotating)
        {
            isRotating = true;

            // Ќачать вращение с учетом начального угла
            transform.DOLocalRotate(new Vector3(0f, 360f, 0f), rotationDuration, RotateMode.WorldAxisAdd)
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
