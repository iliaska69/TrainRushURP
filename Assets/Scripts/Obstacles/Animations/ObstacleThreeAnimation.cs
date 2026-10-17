using DG.Tweening;
using UnityEngine;

public class ObstacleThreeAnimation : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private float jumpDuration;
    [SerializeField] private float firstJumpHeight;
    [SerializeField] private float secondJumpHeight;
    [SerializeField] private Transform firstJumpTargetPointTransform;
    [SerializeField] private Transform secondJumpTargetPointTransform;
    [SerializeField] private Transform spawnPointTransform;
    [SerializeField] private Ease jumpType;


    private void Start()
    {
        Invoke("FirstJump", delay);
    }

    private void FirstJump()
    {
        transform.DOLocalJump(firstJumpTargetPointTransform.localPosition, firstJumpHeight, 1, jumpDuration)
            .SetEase(jumpType)
            .OnComplete(() => SecondJump());

        transform.DOLocalRotate(new Vector3(0f, 0f, -90f), jumpDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear);
    }

    private void SecondJump()
    {
        transform.DOLocalJump(secondJumpTargetPointTransform.localPosition, secondJumpHeight, 1, jumpDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => OnCompliteJump());

        transform.DOLocalRotate(new Vector3(0f, 0f, 180f), jumpDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear);
    }

    private void OnCompliteJump()
    {
        transform.localPosition = spawnPointTransform.localPosition;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        Invoke("FirstJump", 1f);
    }
}
