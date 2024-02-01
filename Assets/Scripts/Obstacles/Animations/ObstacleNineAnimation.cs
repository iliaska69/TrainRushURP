using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleNineAnimation : MonoBehaviour
{
    [SerializeField] private List<GameObject> trampolines = new List<GameObject>();
    [SerializeField] private float jumpDuration;
    [SerializeField] private float jumpHeight;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 secondPosition;
    [SerializeField] private Vector3 lastPosition;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
        Invoke("FirstJump", 1f);
    }
    //не доделан, дождаться батутов
    private void FirstJump()
    {
        _transform.DOLocalJump(secondPosition, jumpHeight, 1, jumpDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => SecondJump());

        _transform.DOLocalRotate(new Vector3(0f, 0f, -90f), jumpDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear);

        //trampolines[0].transform
        //    .DOMoveY(trampolines[0].transform.position.y + 0.5f, 0.3f)
        //    .SetEase(Ease.OutQuad)
        //    .OnComplete(
        //        () => trampolines[0].transform
        //        .DOMoveY(trampolines[0].transform.position.y - 0.5f, 1f)
        //        .SetEase(Ease.Linear));
    }

    private void SecondJump()
    {
        _transform.DOLocalJump(lastPosition, jumpHeight, 1, jumpDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => ThreeJump());

        _transform.DOLocalRotate(new Vector3(0f, 0f, 180f), jumpDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear);

        //trampolines[1].transform
        //    .DOMoveY(trampolines[1].transform.position.y + 0.5f, 0.3f)
        //    .SetEase(Ease.OutQuad)
        //    .OnComplete(
        //        () => trampolines[1].transform
        //        .DOMoveY(trampolines[1].transform.position.y - 0.5f, 1f)
        //        .SetEase(Ease.Linear));
    }

    private void ThreeJump()
    {
        _transform.DOLocalJump(secondPosition, jumpHeight, 1, jumpDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => FourJump());

        _transform.DOLocalRotate(new Vector3(0f, 0f, 270f), jumpDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear);
    }

    private void FourJump()
    {
        _transform.DOLocalJump(startPosition, jumpHeight, 1, jumpDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => FirstJump());

        _transform.DOLocalRotate(new Vector3(0f, 0f, 360f), jumpDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear);
    }
}

