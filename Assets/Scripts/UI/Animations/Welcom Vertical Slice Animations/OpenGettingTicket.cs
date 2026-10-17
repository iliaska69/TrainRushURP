using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class OpenGettingTicket : UIAnimation
{
    [SerializeField] private Image ticketImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private CanvasGroup canvasGroup;
    
    private void Start()
    {
        ticketImage.transform.localScale = Vector3.zero;
        canvasGroup.alpha = 0f;

        OpenAnimation(openDuration);
    }

    // public override void CloseAnimation(float duration)
    // {
    //     var sequence = DOTween.Sequence();
    //     sequence.Append(ticketImage.transform.DOScale(0.3f, duration).SetEase(Ease.OutBack));
    //     sequence.Join(canvasGroup.DOFade(0f, duration));
    // }
    //
    // public override void OpenAnimation(float duration)
    // {
    //     var sequence = DOTween.Sequence();
    //     sequence.Append(ticketImage.transform.DOScale(0.3f, duration).SetEase(Ease.Linear));
    //     sequence.Join(canvasGroup.DOFade(1f, duration));
    // }
    
    [Button("Close")]
    public void CloseAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(ticketImage.transform.DOScale(0.3f, 0.2f).SetEase(Ease.OutBack));
        sequence.Join(canvasGroup.DOFade(0f, 0.2f));
        sequence.Join(backgroundImage.DOFade(0f, 0.2f));
    }

    [Button("Open")]
    public void OpenAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(ticketImage.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack));
        sequence.Join(canvasGroup.DOFade(1f, 0.5f));
        sequence.Join(backgroundImage.DOFade(0.7f, 0.5f));
    }
}
