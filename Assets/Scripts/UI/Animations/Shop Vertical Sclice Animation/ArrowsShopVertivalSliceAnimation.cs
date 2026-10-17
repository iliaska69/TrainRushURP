using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ArrowsShopVertivalSliceAnimation : UIAnimation
{
    [SerializeField] private RectTransform _rectTransformArrow1;
    [SerializeField] private RectTransform _rectTransformArrow2;
    
    
    private void Start()
    {
        _rectTransformArrow1.anchoredPosition += new Vector2(-400, 0);
        _rectTransformArrow2.anchoredPosition += new Vector2(400, 0);
        
        OpenAnimation(openDuration);
    }
    
    public override void CloseAnimation(float duration)
    {
        _rectTransformArrow1.DOAnchorPos(new Vector2(-400, 0), duration).SetEase(Ease.InOutBack);
        _rectTransformArrow1.DOAnchorPos(new Vector2(400, 0), duration).SetEase(Ease.InOutBack);
    }

    public override void OpenAnimation(float duration)
    {
        StartCoroutine(Wait(duration));
    }
    
    IEnumerator Wait(float duration)
    {
        yield return new WaitForSecondsRealtime(0.3f);
        
        _rectTransformArrow1.DOAnchorPos(new Vector2(300, 0), duration).SetEase(Ease.InOutBack);
        
        _rectTransformArrow2.DOAnchorPos(new Vector2(-300, 0), duration).SetEase(Ease.InOutBack);
    }
}
