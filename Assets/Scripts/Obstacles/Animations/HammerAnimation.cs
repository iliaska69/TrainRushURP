using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerAnimation : MonoBehaviour
{
    [SerializeField] private float hitDuration;
    [SerializeField] private Vector3 hitRotation = new Vector3(0f, 0f, 90f);
    [SerializeField] private Ease hitEaseType;
    [SerializeField] private Ease returningEaseType;

    private void Start()
    {
        Invoke("HammerHit", 2f);
    }

    private void HammerHit()
    {
        Sequence hammerSequence = DOTween.Sequence();

        hammerSequence.Append(
            transform
            .DORotate(hitRotation, hitDuration)
            .SetEase(hitEaseType));

        hammerSequence.Append(
            transform
            .DORotate(Vector3.zero, hitDuration)
            .SetEase(returningEaseType)
            .OnComplete(() => Invoke("HammerHit", 2f)));

        hammerSequence.Play();
    }
}
