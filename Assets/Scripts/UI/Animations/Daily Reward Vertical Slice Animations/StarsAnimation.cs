using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class StarsAnimation : MonoBehaviour
{
    public RectTransform star1;
    public RectTransform star2;
    public RectTransform star3;
    public RectTransform star4;
    public float moveDistance = 100f;
    public float duration = 2f;
    [SerializeField] private float delay0; 
    [SerializeField] private float delay1; 
    [SerializeField] private float delay2; 
    [SerializeField] private float delay3; 

    private void Start()
    {
        AnimateStars();
    }

    private async void AnimateStars()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(delay0));
        AnimateStar(star1);
        await UniTask.Delay(TimeSpan.FromSeconds(delay1));
        AnimateStar(star2);
        await UniTask.Delay(TimeSpan.FromSeconds(delay2));
        AnimateStar(star3);
        await UniTask.Delay(TimeSpan.FromSeconds(delay3));
        AnimateStar(star4);
    }
    
    private void AnimateStar(RectTransform star)
    {
        if(star == null)
            return;
        
        var initialPosition = star.anchoredPosition;
        var canvasGroup = star.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        star.localScale = new Vector3(0.5f, 0.5f,0.5f);
        
        var sequence = DOTween.Sequence();
        
        sequence.Append(canvasGroup.DOFade(1, duration / 2));
        sequence.Join(star.DOAnchorPosY(initialPosition.y + moveDistance, duration).SetEase(Ease.InOutSine));
        sequence.Join(star.DOScale(Vector3.one * 1.5f, duration / 2));
        sequence.Join(canvasGroup.DOFade(0, duration / 2).SetDelay(duration / 2));
        sequence.Join(star.DOScale(Vector3.one * 0.5f, duration / 2)).SetDelay(duration / 2);
        sequence.SetLoops(-1);
    }
}
