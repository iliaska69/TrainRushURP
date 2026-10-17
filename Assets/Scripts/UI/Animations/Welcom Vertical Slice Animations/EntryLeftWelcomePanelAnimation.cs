using DG.Tweening;
using UnityEngine;

public class EntryLeftWelcomePanelAnimation : UIAnimation
{
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        OpenAnimation(openDuration);
    }

    public override void CloseAnimation(float duration)
    {
        _rectTransform.DOAnchorPos(new Vector2(-300, 0), duration).SetEase(Ease.InOutBack);
    }

    public override void OpenAnimation(float duration)
    {
        if(_isOpenAnimation)
            return;
        
        _isOpenAnimation = true;
        _rectTransform.anchoredPosition = new Vector2(-300, 0);
        _rectTransform.DOAnchorPos(new Vector2(0, 0), duration).SetEase(Ease.InOutBack);
    }
}
