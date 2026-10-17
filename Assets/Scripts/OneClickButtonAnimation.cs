using DG.Tweening;
using UnityEngine;

public class OneClickButtonAnimation : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private SfxSound dailyRewardSfxSound;

    private Transform buttonTransform;
    private bool _buttonIsDone;

    private void Start()
    {
        buttonTransform = transform;
    }

    public void OnButtonClick()
    {
        if (_buttonIsDone)
            return;
        
        dailyRewardSfxSound.Play();
        
        _buttonIsDone = true;
        
        var currentScale = buttonTransform.localScale;
        var reducedScale = currentScale * 0.9f;
        
        buttonTransform
            .DOScale(reducedScale, duration / 2)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                buttonTransform.DOScale(currentScale, duration).SetEase(Ease.InOutQuad);
            });
    }
}
