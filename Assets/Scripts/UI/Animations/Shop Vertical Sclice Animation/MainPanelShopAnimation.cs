using System.Collections;
using DG.Tweening;
using UnityEngine;

public class MainPanelShopAnimation : UIAnimation
{
    [SerializeField] private RectTransform _rectTransformArrow1;
    [SerializeField] private RectTransform _rectTransformArrow2;
    
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        _canvasGroup.alpha = 0f;
        _rectTransform.anchoredPosition = new Vector2(0, -1100);
        
        _rectTransformArrow1.anchoredPosition += new Vector2(-400, 0);
        _rectTransformArrow2.anchoredPosition += new Vector2(400, 0);
        
        OpenAnimation(openDuration);
    }

    public override void CloseAnimation(float duration)
    {
        StartCoroutine(CloseSliceAnim());
    }

    IEnumerator CloseSliceAnim()
    {
        _rectTransformArrow1.DOAnchorPos(new Vector2(-400, 0), 0.2f).SetEase(Ease.Linear);
        _rectTransformArrow2.DOAnchorPos(new Vector2(400, 0),  0.2f).SetEase(Ease.Linear);
        
        yield return new WaitForSecondsRealtime(0.1f);
        _rectTransform.DOAnchorPos(new Vector2(0, -900), closeDuration);
        _rectTransform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), closeDuration);
        _canvasGroup.DOFade(0, closeDuration);
    }
    
    public override void OpenAnimation(float duration)
    {
        StartCoroutine(OpenSliceAnim(duration));
    }

    IEnumerator OpenSliceAnim(float duration)
    {
        yield return new WaitForSecondsRealtime(0.15f);
        
        _canvasGroup.DOFade(1, duration);
        _rectTransform.DOAnchorPos(new Vector2(0, 0), duration).SetEase(Ease.Linear);
        _rectTransform.DOScale(new Vector3(1, 1, 1), duration);
        
        yield return new WaitForSecondsRealtime(0.15f);
        
        _rectTransformArrow1.DOAnchorPos(new Vector2(300, 0), duration).SetEase(Ease.InOutBack);
        _rectTransformArrow2.DOAnchorPos(new Vector2(-300, 0), duration).SetEase(Ease.InOutBack);
    }
}
