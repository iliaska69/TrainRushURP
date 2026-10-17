using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EntryDailyRewardVerticalSliceAnimation : UIAnimation
{
    [SerializeField] private GameObject animationWrapper;
    [SerializeField] private Image backgroundSpriteRenderer;
    [SerializeField] private float fade;
    
    private CanvasGroup _canvasGroup;
    private CanvasGroup _canvasGroupSecondWrapper;
    private CanvasGroup _canvasGroupAnimationWrapper3;
    
    private RectTransform _rectTransform;
    private RectTransform _secondWrapperRectTransform;
    private RectTransform _animationWrapper3RectTransform;
    
    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroupSecondWrapper = animationWrapper.GetComponent<CanvasGroup>();
        
        _rectTransform = GetComponent<RectTransform>();
        _secondWrapperRectTransform = animationWrapper.GetComponent<RectTransform>();
        
        _rectTransform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        _secondWrapperRectTransform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        _canvasGroup.alpha = 0f;
        _canvasGroupSecondWrapper.alpha = 0f;
        
        OpenAnimation(openDuration);
    }
    
    public override void CloseAnimation(float duration)
    {
        _rectTransform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), closeDuration);
        _secondWrapperRectTransform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), closeDuration);
        
        _canvasGroup.DOFade(0, closeDuration);
        _canvasGroupSecondWrapper.DOFade(0, closeDuration);
        
        backgroundSpriteRenderer.DOFade(0, closeDuration);
    }

    public override void OpenAnimation(float duration)
    {
        _canvasGroup.DOFade(1, openDuration);
        _canvasGroupSecondWrapper.DOFade(1, openDuration);
        
        backgroundSpriteRenderer.DOFade(fade, openDuration);
        
        _rectTransform.DOScale(new Vector3(1, 1, 1), openDuration);
        _secondWrapperRectTransform.DOScale(new Vector3(1, 1, 1), openDuration);
    }
}
