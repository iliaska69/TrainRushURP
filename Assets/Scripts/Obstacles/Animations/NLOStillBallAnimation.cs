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
    [SerializeField] private float levitationAngle;
    [SerializeField] private float levitationDuration = 2.0f;

    private Quaternion _startRotation;
    private Vector3 _endPos;


    void Start()
    {
        _endPos = transform.position;
        _startRotation = nloAnimatedObject.transform.rotation;
        TakeOff();
        LevitationNlo();
    }

    void TakeOff()
    {
        ballTransform
            .DOMoveY(nloTargetTransform.transform.position.y, 1.8f)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() => BallBalancing());
    }

    void BallBalancing()
    {
        Sequence mySequence = DOTween.Sequence();
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
            .OnComplete(() => Invoke("TakeOff", 0.5f));
    }


    void LevitationNlo()
    {
        Quaternion levitationRotation1 = _startRotation * Quaternion.Euler(0, 0, levitationAngle);
        Quaternion levitationRotation2 = _startRotation * Quaternion.Euler(0, 0, -levitationAngle);

        nloAnimatedObject.transform.DORotateQuaternion(levitationRotation1, levitationDuration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(
                () => nloAnimatedObject.transform.DORotateQuaternion(levitationRotation2, levitationDuration)
                .SetEase(Ease.InOutQuad)
                .OnComplete(
                    () => Invoke("LevitationNlo", 0.1f)));

    }
}
