using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleThreeAnimation : MonoBehaviour
{
    [SerializeField] private float jumpDuration;
    [SerializeField] private float jumpHeight;
    [SerializeField] private Vector3 secondPosition;
    [SerializeField] private Ease jumpType;


    private void Start()
    {
        Invoke("FirstJump", 1f);
    }

    private void FirstJump()
    {
        transform.DOLocalJump(secondPosition, jumpHeight, 1, jumpDuration)
            .SetEase(jumpType)
            .OnComplete(() => OnCompliteJump());

        transform.DOLocalRotate(new Vector3(0f, 0f, -90f), jumpDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear);
    }

    private void OnCompliteJump()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.EulerRotation(-9.218f, 2.634f, -0.209f);

        Invoke("FirstJump", 1f);
    }
}
