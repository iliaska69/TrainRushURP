using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerAnimation : MonoBehaviour
{
    [SerializeField] private Transform hummerTransform;
    [SerializeField] private float hitDuration;
    [SerializeField] private Vector3 hitRotation;
    [SerializeField] private Ease hitEaseType;
    [SerializeField] private Ease returningEaseType;
    [SerializeField] private float startDelay = 2f;

    private Vector3 _startRotation;

    private void Start()
    {
        _startRotation = hummerTransform.localRotation.eulerAngles;
        Invoke("HammerHit", startDelay);
    }

    private void HammerHit()
    {
        var hammerSequence = DOTween.Sequence();
        
        hammerSequence.Append(
            hummerTransform
            .DOLocalRotate(hitRotation, hitDuration)
            .SetEase(hitEaseType));
        
        hammerSequence.Append(
            hummerTransform
            .DOLocalRotate(_startRotation, hitDuration)
            .SetEase(returningEaseType)
            .OnComplete(() => Invoke("HammerHit", 2f)));
        
        hammerSequence.Play();
    }
}
