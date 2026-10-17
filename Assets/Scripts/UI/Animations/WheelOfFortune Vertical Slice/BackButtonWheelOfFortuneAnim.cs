using DG.Tweening;
using UnityEngine;

public class BackButtonWheelOfFortuneAnim : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float duration;

    private Vector3 startValue;
    
    private void Start()
    {
        startValue = rectTransform.localScale;
    }

    public void ShowButton()
    {
        canvasGroup.DOFade(1, duration);
        rectTransform.DOScale(startValue, duration);
    }
    
    public void HideButton()
    {
        rectTransform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), duration);
        canvasGroup.DOFade(0, duration);
    }
}
