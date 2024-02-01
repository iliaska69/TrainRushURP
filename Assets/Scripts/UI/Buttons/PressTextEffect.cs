using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressTextEffect : MonoBehaviour
{
    [SerializeField] private Transform pressTextTransform;

    private Vector3 originalScale;
    private Vector3 pressingScale;
    private Vector3 transformPath;

    private void Start()
    {
        originalScale = pressTextTransform.localScale;
        pressingScale = originalScale * 1.5f;

        PressEffect();
    }

    public void PressEffect()
    {
        pressTextTransform
            .DOScale(pressingScale, 0.5f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        pressTextTransform.DOKill();
    }
}
