using DG.Tweening;
using UnityEngine;

public class GettingTicketAnim : MonoBehaviour
{
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Vector3 _startLocalPosition;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _startLocalPosition = _rectTransform.localPosition;
    }

    public void EntryTicketImageAnim()
    {
        var sequence = DOTween.Sequence();
        sequence
            .Append(_canvasGroup.DOFade(1, 0.5f).SetEase(Ease.Linear))
            .Join(_rectTransform.DOLocalMove(_startLocalPosition + new Vector3(0, 100, 0), 0.5f).SetEase(Ease.Linear))
           
            .Append(_rectTransform.DOLocalMove(_startLocalPosition + new Vector3(0, 200, 0), 0.5f).SetEase(Ease.Linear))
            
            .Append(_rectTransform.DOLocalMove(_startLocalPosition + new Vector3(0, 300, 0), 0.5f).SetEase(Ease.Linear))
            .Join(_canvasGroup.DOFade(0, 0.5f).SetEase(Ease.Linear));
        _rectTransform.localPosition = _startLocalPosition;
    }
}