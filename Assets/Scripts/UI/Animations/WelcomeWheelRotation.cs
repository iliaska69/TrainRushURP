using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class WelcomeWheelRotation : MonoBehaviour
{
    [SerializeField] private Transform slots;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        StartCoroutine(WaitingCor());
        
        
    }
    
    
    public void AnimateRoulette()
    {
        transform.DOScale(originalScale * 1.15f, 0.5f)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                slots.DORotate(new Vector3(0, 0, 1080), 0.8f, RotateMode.FastBeyond360)
                    .SetEase(Ease.InOutCubic)
                    .OnComplete(() =>
                    {
                       
                        transform.DOScale(originalScale, 0.5f)
                            .SetEase(Ease.InBack);
                    });
            });
    }

    IEnumerator WaitingCor()
    {
        yield return new WaitForSeconds(3f);
        while (true)
        {
            AnimateRoulette();
            yield return new WaitForSeconds(6f);
        }
        // InvokeRepeating(nameof(AnimateRoulette), 0f, 6f);
    }
}
