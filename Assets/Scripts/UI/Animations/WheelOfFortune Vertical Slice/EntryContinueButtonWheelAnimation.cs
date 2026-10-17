using DG.Tweening;
using UnityEngine;

public class EntryContinueButtonWheelAnimation : UIAnimation
{
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.localScale = new Vector3(0, 0, 0);
        OpenAnimation(openDuration);
    }

    public override void CloseAnimation(float duration)
    {
        _rectTransform.DOScale(new Vector3(0, 0, 0), openDuration);
    }

    public override void OpenAnimation(float duration)
    {
        _rectTransform.DOScale(new Vector3(1, 1, 1), openDuration);
    }
}
