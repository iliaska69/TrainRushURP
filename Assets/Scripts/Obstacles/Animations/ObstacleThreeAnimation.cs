using DG.Tweening;
using UnityEngine;

public class ObstacleThreeAnimation : MonoBehaviour
{
    [SerializeField] private float jumpDuration;
    [SerializeField] private float firstJumpHeight;
    [SerializeField] private float secondJumpHeight;
    [SerializeField] private Transform firstJumpTargetPointTransform;
    [SerializeField] private Transform secondJumpTargetPointTransform;
    [SerializeField] private Transform spawnPointTransform;
    [SerializeField] private Ease jumpType;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
        Invoke("FirstJump", 1f);
    }

    private void FirstJump()
    {
        _transform.DOLocalJump(firstJumpTargetPointTransform.localPosition, firstJumpHeight, 1, jumpDuration)
            .SetEase(jumpType)
            .OnComplete(() => SecondJump());

        _transform.DOLocalRotate(new Vector3(0f, 0f, -90f), jumpDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear);
    }

    private void SecondJump()
    {
        _transform.DOLocalJump(secondJumpTargetPointTransform.localPosition, secondJumpHeight, 1, jumpDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => OnCompliteJump());

        _transform.DOLocalRotate(new Vector3(0f, 0f, 180f), jumpDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear);
    }

    private void OnCompliteJump()
    {
        _transform.localPosition = spawnPointTransform.localPosition;
        _transform.rotation = Quaternion.Euler(0, 0, 0);

        Invoke("FirstJump", 1f);
    }
}
