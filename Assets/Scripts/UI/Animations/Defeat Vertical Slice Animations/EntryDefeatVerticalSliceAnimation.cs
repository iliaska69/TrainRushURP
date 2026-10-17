using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EntryDefeatVerticalSliceAnimation : UIAnimation
{
    [SerializeField] private GameObject animationWrapper;
    [SerializeField] private float delayBetweenAnimations = 2f;
    [SerializeField] private Image backgroundSpriteRenderer;
    [SerializeField] private float fade;
    
    private RectTransform _rectTransform;
    private RectTransform _animationWrapperTransform;
    
    
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _animationWrapperTransform =  animationWrapper.GetComponent<RectTransform>();
        _rectTransform.localScale = new Vector3(0, 0, 0);
        _animationWrapperTransform.localScale = new Vector3(0, 0, 0);
        
        OpenAnimation(openDuration);
    }
    
    public override void CloseAnimation(float duration)
    {
        _rectTransform.DOScale(new Vector3(0, 0, 0), closeDuration);
        _animationWrapperTransform.DOScale(new Vector3(0, 0, 0), closeDuration);
        backgroundSpriteRenderer.DOFade(0, closeDuration);
    }

    public override void OpenAnimation(float duration)
    {
        var sequence = DOTween.Sequence();
        backgroundSpriteRenderer.DOFade(fade, openDuration);
        sequence.Append(_rectTransform.DOScale(new Vector3(1, 1, 1), openDuration));
        sequence.AppendInterval(delayBetweenAnimations);
        sequence.Append(_animationWrapperTransform.DOScale(new Vector3(1, 1, 1), openDuration));
        sequence.Play();
    }
}
