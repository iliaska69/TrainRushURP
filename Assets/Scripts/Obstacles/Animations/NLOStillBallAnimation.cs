using DG.Tweening;
using UnityEngine;

public class NLOStillBallAnimation : MonoBehaviour
{
    [SerializeField] private Transform ballTransform;
    [SerializeField] private Transform target;
    [SerializeField] private float duration;
    [SerializeField] private float power;
    [SerializeField] private Transform nloTargetTransform;
    [SerializeField] private GameObject nloAnimatedObject;
    [SerializeField] private float startDuration;

    private Quaternion _startRotation;
    private Vector3 _endPos;

    public float levitationAngle;
    public float levitationDuration = 2.0f;

    void Start()
    {
        _endPos = transform.position;
        _startRotation = nloAnimatedObject.transform.rotation;
        Invoke(nameof(TakeOff), startDuration);
        LevitationNlo();
    }

    void TakeOff()
    {
        ballTransform
            .DOMoveY(nloTargetTransform.transform.position.y, 1.8f)
            .SetEase(Ease.InOutCubic)
            .OnComplete(BallBalancing);
    }

    void BallBalancing()
    {
        ballTransform
            .DOMoveY(ballTransform.position.y - 1f, 1f)
            .SetEase(Ease.InOutSine)
            .SetLoops(5, LoopType.Yoyo)
            .OnComplete(() => ThrowBall());
    }

    void ThrowBall()
    {
        ballTransform
            .DOJump(_endPos, power, 1, duration)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => Invoke(nameof(TakeOff), 0.5f));
    }


    void LevitationNlo()
    {
        var levitationRotation1 = _startRotation * Quaternion.Euler(0, 0, levitationAngle);
        var levitationRotation2 = _startRotation * Quaternion.Euler(0, 0, -levitationAngle);

        nloAnimatedObject.transform.DORotateQuaternion(levitationRotation1, levitationDuration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(
                () => nloAnimatedObject.transform.DORotateQuaternion(levitationRotation2, levitationDuration)
                .SetEase(Ease.InOutQuad)
                .OnComplete(
                    () => Invoke(nameof(LevitationNlo), 0.1f)));
    }
}
