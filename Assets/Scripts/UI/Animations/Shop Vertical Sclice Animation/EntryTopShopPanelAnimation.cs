using DG.Tweening;
using UnityEngine;

public class EntryTopShopPanelAnimation : UIAnimation
{
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        OpenAnimation(openDuration);
    }

    public override void CloseAnimation(float duration)
    {
        _rectTransform.DOAnchorPos(new Vector2(0, 600), duration).SetEase(Ease.InOutBack);
    }

    public override void OpenAnimation(float duration)
    {
        _rectTransform.anchoredPosition = new Vector2(0, 600);
        _rectTransform.DOAnchorPos(new Vector2(0, 0), duration).SetEase(Ease.InOutBack);
    }
}
