using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EntryVictoryVerticalSliceAnimation : UIAnimation
{
    [SerializeField] private Image backgroundSpriteRenderer;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fade;
    [SerializeField] private GameObject noThanksAnimationWrapper;
    [SerializeField] private float delayBetweenAnimations = 2f;
    
    private RectTransform _rectTransform;
    private RectTransform _noThanksAnimationWrapperTransform;
    
    
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.localScale = new Vector3(0, 0, 0);
        
        _noThanksAnimationWrapperTransform = noThanksAnimationWrapper.GetComponent<RectTransform>();
        _noThanksAnimationWrapperTransform.localScale = new Vector3(0, 0, 0);
        canvasGroup.alpha = 0.5f;
        
        OpenAnimation(openDuration);
    }
    
    public override void CloseAnimation(float duration)
    {
        _rectTransform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), closeDuration);
        _noThanksAnimationWrapperTransform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), closeDuration);
        backgroundSpriteRenderer.DOFade(0, closeDuration);
        
        canvasGroup.DOFade(0, closeDuration);
    }

    public override void OpenAnimation(float duration)
    {
         backgroundSpriteRenderer.DOFade(fade, openDuration);
         canvasGroup.DOFade(1, openDuration);
         
         var sequence = DOTween.Sequence();
         sequence.Append(_rectTransform.DOScale(new Vector3(1, 1, 1), openDuration));
         sequence.AppendInterval(delayBetweenAnimations);
         sequence.Append(_noThanksAnimationWrapperTransform.DOScale(new Vector3(1, 1, 1), openDuration));
         sequence.Play();
    }
}
