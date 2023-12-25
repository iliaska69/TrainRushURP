using DG.Tweening;
using UnityEngine;

public class ObstacleNineAnimation : MonoBehaviour
{
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
    }

    private void SecondJump()
    {
        transform.DOLocalJump(lastPosition, jumpHeight, 1, jumpDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => ThreeJump());

        transform.DOLocalRotate(new Vector3(0f, 0f, 180f), jumpDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear);
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

