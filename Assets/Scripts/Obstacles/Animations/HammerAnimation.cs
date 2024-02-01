using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HammerAnimation : MonoBehaviour
{
    [SerializeField] private float hitDuration;
    [SerializeField] private Vector3 hitRotation = new Vector3(0f, 0f, 90f);
    [SerializeField] private Ease hitEaseType;
    [SerializeField] private Ease returningEaseType;
    [SerializeField] private float InvokeDelay = 1f;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
        Invoke("HammerHit", InvokeDelay);
    }

    private void HammerHit()
    {
        Sequence hammerSequence = DOTween.Sequence();

        hammerSequence.Append(
            _transform
            .DORotate(hitRotation, hitDuration)
            .SetEase(hitEaseType));

        hammerSequence.Append(
            _transform
            .DORotate(Vector3.zero, hitDuration)
            .SetEase(returningEaseType)
            .OnComplete(() => Invoke("HammerHit", 2f)));

        hammerSequence.Play();
    }
}
