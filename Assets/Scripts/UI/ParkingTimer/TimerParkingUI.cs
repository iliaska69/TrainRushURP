using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class TimerParkingUI : MonoBehaviour
{
    [Header("Duration")]
    [SerializeField] private float fillDuration = 1f;
    [SerializeField] private float delayDuration = 0.4f;
    
    [Header("Fill Image")]
    [SerializeField] private Image fillImage;
    [SerializeField] private List<Color> colors;

    [Header("Press Button")]
    [SerializeField] private GameObject pressButton;

    [Header("Number")]
    [SerializeField] private TextMeshProUGUI timerText;
    
    [Header("Sound")]
    [SerializeField] private AdditionalMusicSound timerSound;
    
    [Header("Canvas Groups")]
    [SerializeField] private CanvasGroup timerCanvasGroup;
    [SerializeField] private CanvasGroup pressButtonCanvasGroup;
    
    private float _lastLoggedTime;
    private float _colorChangePerTime;
    private AudioSource _timerAudioSource;
    
    void Start()
    {
        fillImage.fillAmount = 1f;
        fillImage.color = Color.green;
        _colorChangePerTime = fillDuration / colors.Count;
        _timerAudioSource = timerSound.Play();
        
        TimerAnimation();
        FillImageAnimation();
        ChangeColorFillImageAnimation();
    }

    private void OnDestroy()
    {
        _timerAudioSource.Stop();
    }

    private void ChangeColorFillImageAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence
            .AppendInterval(delayDuration)
            .AppendInterval(_colorChangePerTime)
            .Append(ChangeColor(Color.yellow, 0.2f))
            .AppendInterval(_colorChangePerTime)
            .Append(ChangeColor(Color.red, 0.2f));
    }

    private Tweener ChangeColor(Color color, float duration)
    {
        return fillImage.DOColor(color, duration);
    }
    
    private void FillImageAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence
            .AppendInterval(delayDuration)
            .Append(fillImage.DOFillAmount(0f, fillDuration)
                .SetEase(Ease.Linear));
    }

    private void TimerAnimation()
    {
        TextTimer();
    }

    private void TextTimer()
    {
        DOVirtual.DelayedCall(delayDuration, () =>
        {
            DOTween.To(() => fillDuration, x =>
                {
                    fillDuration = x;
                    timerText.text = Mathf.Ceil(fillDuration).ToString();
                }, 0, fillDuration)
                .SetEase(Ease.Linear)
                .OnUpdate(() => 
                {
                    if (fillDuration > 9f) return;
                    var currentTime = Mathf.CeilToInt(fillDuration);

                    if (currentTime == _lastLoggedTime) return;

                    _lastLoggedTime = currentTime;
                    TextPunchEffect();
                });
        });
    }
    
    private void TextPunchEffect()
    {
        timerText.rectTransform
            .DOPunchScale(new Vector3(.5f, .5f), 0.2f)
            .SetEase(Ease.InOutElastic);
    }

    public void TimerStop()
    {
        timerCanvasGroup.DOFade(0, 0.4f);
        pressButtonCanvasGroup.DOFade(0, 0.4f).OnComplete(() =>
        {
            pressButtonCanvasGroup.gameObject.SetActive(false);
        });
        _timerAudioSource.Stop();
    }
}
