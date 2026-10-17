using DG.Tweening;
using TMPro;
using UnityEngine;

public class TapToPlayButtonAnimation : UIAnimation
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] RectTransform _handRectTransform;
    [SerializeField] RectTransform _textRectTransform;
    [SerializeField] private RectTransform _rectTransform;

    private Vector3 _startScale;
    
    void Start()
    {
        _startScale = transform.localScale;
        OpenAnimation(openDuration);
    }

    public override void CloseAnimation(float duration)
    {
        canvasGroup.DOFade(0, duration);
        _rectTransform
            .DOScale(Vector2.zero, duration);
    }

    public override void OpenAnimation(float duration)
    {
        _rectTransform.localScale = Vector2.zero;
        
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1, duration);
        
        _rectTransform
            .DOScale(_startScale, duration);

        Blinking();
        AnimateHand();
    }

    void Blinking()
    {
        _textRectTransform.DOScale(new Vector3(1.2f, 1.2f, 1), 0.8f) 
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
    
    void AnimateHand()
    {
        // _handRectTransform.DORotate(new Vector3(0, 0, -10), 1f)
        //     .SetEase(Ease.InOutSine)
        //     .SetLoops(-1, LoopType.Yoyo);
    
        _handRectTransform.DOScale(new Vector3(1.1f, 1.1f, 1), 0.8f) 
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
