using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleNineAnimation : MonoBehaviour
{
    [SerializeField] private List<GameObject> trampolines = new List<GameObject>();
    [SerializeField] private float jumpDuration;
    [SerializeField] private float jumpHeight;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 secondPosition;
    [SerializeField] private Vector3 lastPosition;

    private void Start()
    {
        Invoke("FirstJump", 1f);
    }

    private void FirstJump()
    {
        transform.DOLocalJump(secondPosition, jumpHeight, 1, jumpDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => SecondJump());

        transform.DOLocalRotate(new Vector3(0f, 0f, -90f), jumpDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear);

        trampolines[0].transform
            .DOMoveY(trampolines[0].transform.position.y + 0.5f, 0.3f)
            .SetEase(Ease.OutQuad)
            .OnComplete(
                () => trampolines[0].transform
                .DOMoveY(trampolines[0].transform.position.y - 0.5f, 1f)
                .SetEase(Ease.Linear));
    }

    private void SecondJump()
    {
        transform.DOLocalJump(lastPosition, jumpHeight, 1, jumpDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => ThreeJump());

        transform.DOLocalRotate(new Vector3(0f, 0f, 180f), jumpDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear);

        trampolines[1].transform
            .DOMoveY(trampolines[1].transform.position.y + 0.5f, 0.3f)
            .SetEase(Ease.OutQuad)
            .OnComplete(
                () => trampolines[1].transform
                .DOMoveY(trampolines[1].transform.position.y - 0.5f, 1f)
                .SetEase(Ease.Linear));
    }

    private void ThreeJump()
    {
        transform.DOLocalJump(secondPosition, jumpHeight, 1, jumpDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => FourJump());

        transform.DOLocalRotate(new Vector3(0f, 0f, 270f), jumpDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear);
    }

    private void FourJump()
    {
        transform.DOLocalJump(startPosition, jumpHeight, 1, jumpDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => FirstJump());

        transform.DOLocalRotate(new Vector3(0f, 0f, 360f), jumpDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear);
    }
}

