using DG.Tweening;
using UnityEngine;

public class SimpleClickButtonAnimation : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float putValue;
    [SerializeField] private float backValue;
    [SerializeField] private SfxSound sfxSound;
    
    private Transform buttonTransform;
    private Tweener scaleTween;
    private bool _isLock;
    private bool isAnimating = false;  

    private void Start()
    {
        buttonTransform = gameObject.GetComponent<Transform>();
    }

    public void OnButtonClick()
    {
        if (isAnimating)
            return;
        isAnimating = true;
        
        if (sfxSound != null) sfxSound.Play();
        scaleTween?.Kill();

        buttonTransform = gameObject.GetComponent<Transform>();

        var currentScale = buttonTransform.localScale;

        var reducedScale = currentScale * 0.9f;

        scaleTween = buttonTransform.DOScale(reducedScale, duration / 2)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                buttonTransform.DOScale(currentScale, duration).SetEase(Ease.InOutQuad)
                    .OnComplete(() =>
                    {
                        isAnimating = false;
                    });
            });
    }
}
