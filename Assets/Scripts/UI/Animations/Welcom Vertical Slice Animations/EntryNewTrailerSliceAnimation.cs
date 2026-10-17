using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EntryNewTrailerSliceAnimation : UIAnimation
{
    [SerializeField] private GameObject animationWrapper1;
    [SerializeField] private float delayBetweenWrappersAnimations = 2f;
    [SerializeField] private Image backgroundSpriteRenderer;
    [SerializeField] private float fade;
    
    private RectTransform _animationWrapperRectTransform;
    private RectTransform _animationWrapper1RectTransform;
    
    
    private void Start()
    {
        _animationWrapperRectTransform = GetComponent<RectTransform>();
        _animationWrapper1RectTransform =  animationWrapper1.GetComponent<RectTransform>();
        
        _animationWrapperRectTransform.localScale = new Vector3(0, 0, 0);
        _animationWrapper1RectTransform.localScale = new Vector3(0, 0, 0);
        
        OpenAnimation(openDuration);
    }
    
    public override void CloseAnimation(float duration)
    {
        _animationWrapperRectTransform.DOScale(new Vector3(0, 0, 0), 0.2f);
        _animationWrapper1RectTransform.DOScale(new Vector3(0, 0, 0),  0.2f);
        backgroundSpriteRenderer.DOFade(0,  0.2f);
    }
    
    public override void OpenAnimation(float duration)
    {
        var sequence = DOTween.Sequence();
        backgroundSpriteRenderer.DOFade(fade,  0.5f);
        sequence.Append(_animationWrapperRectTransform.DOScale(new Vector3(1, 1, 1), 0.5f));
        sequence.AppendInterval(delayBetweenWrappersAnimations);
        sequence.Append(_animationWrapper1RectTransform.DOScale(new Vector3(1, 1, 1), 0.5f));
        sequence.Play();
    }
}
