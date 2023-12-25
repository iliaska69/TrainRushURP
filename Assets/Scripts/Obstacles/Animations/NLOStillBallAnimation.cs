using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NLOStillBallAnimation : MonoBehaviour
{
    [SerializeField] private Transform coreTransform;
    [SerializeField] private Transform target;
    [SerializeField] private float duration;
    [SerializeField] private float power;
    [SerializeField] private Transform nloTargetTransform;
    [SerializeField] private GameObject nloAnimatedObject;
    
    
    private Quaternion _startRotation;
    private Vector3 _endPos;

    public float levitationAngle;
    public float levitationDuration = 2.0f;

    void Start()
    {
        _endPos = transform.position;
        _startRotation = nloAnimatedObject.transform.rotation;
        Jump();
        LevitationNlo();
    }

    void Jump()
    {
        coreTransform
            .DOMoveY(nloTargetTransform.transform.position.y, 1.8f)
            .SetEase(Ease.InOutCubic)
            .OnComplete(()
                => coreTransform.DOJump(_endPos, power, 1, duration)
                                .SetEase(Ease.OutBounce)
                                .OnComplete(() => Invoke("Jump", 2f)));
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
