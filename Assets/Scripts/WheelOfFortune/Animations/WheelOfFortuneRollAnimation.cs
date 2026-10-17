using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WheelOfFortuneRollAnimation : MonoBehaviour
{
    [SerializeField] private int countOfRolls;
    [SerializeField] private VoidEventChannelSO finishRollAnimationEvenSO;
    [SerializeField] private VoidEventChannelSO winWheelAnimationEvenSO;
    [SerializeField] private int angleOfArrow;
    [SerializeField] private RectTransform arrowRectTransform;
    [SerializeField] private SfxSound RollSfx;
    [SerializeField] private SfxSound WheelWinSfx;

    [SerializeField] private TextMeshProUGUI _spinText;
    [SerializeField] private TextMeshProUGUI _tapToSkipRotationText;
    [SerializeField] private float _changeTextDuration = 1f;
    [SerializeField] private float _accelerationMultiplier = 1f;
    [SerializeField] private Button skipRotationButton;
    [SerializeField] private Button spinButton;

    private int _angleOfReward;
    private bool _isRolling;
    private float _rotationZ;
    private int betweenWinAngle;


    private float previousZRotation = 0f;
    private float angleThreshold = 45f;
    private Vector2 originalSize;
    private float targetScaleFactor = 1f;
    private float scaleSpeed = 10f;
    private float cumulativeZRotation = 0f;
    private bool _isTick;

    private Tween _spinTween;
    private float _currentSpeedMultiplier = 1f;
    private Image _spinButtonImage;

    void Start()
    {
        previousZRotation = transform.eulerAngles.z;
        if (arrowRectTransform != null)
        {
            originalSize = arrowRectTransform.sizeDelta;
        }

        _spinButtonImage = spinButton.GetComponent<Image>();

        _tapToSkipRotationText.transform.localScale = Vector3.zero;
        skipRotationButton.transform.localScale = Vector3.zero;
        skipRotationButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        ArrowPulse();
    }

    public void Roll(int winnerElementIndex, int countOfRewards)
    {
        if (_isRolling) return;
        _isRolling = true;
        
        DarkSpinButton();
        EntrySkipRotationButton();
        
        _angleOfReward = 360 / countOfRewards;
        var winnableAngle = _angleOfReward * winnerElementIndex;
        betweenWinAngle = angleOfArrow - winnableAngle;
        var resultAngle = 360 * countOfRolls + betweenWinAngle;

        _spinTween = transform.DORotate(
                new Vector3(0f, 0f, resultAngle),
                countOfRolls * 2f / _currentSpeedMultiplier,
                RotateMode.FastBeyond360)
            .SetEase(Ease.OutCirc)
            .OnComplete(() => OnCompleteRollAnimation());
    }

    public void AccelerateSpin()
    {
        if (_spinTween == null || !_spinTween.IsActive() || !_spinTween.IsPlaying()) return;
        _currentSpeedMultiplier = _accelerationMultiplier;

        var remainingTime = _spinTween.Duration() - _spinTween.Elapsed();
        var newDuration = remainingTime / _currentSpeedMultiplier;

        _spinTween.timeScale = newDuration;
        HideSkipRotationButton();
    }

    private async void OnCompleteRollAnimation()
    {
        _isRolling = false;
        WheelWinSfx.Play();
        winWheelAnimationEvenSO.RaiseEvent();

        HideSkipRotationButton();

        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));

        finishRollAnimationEvenSO.RaiseEvent();
    }

    private void ArrowPulse()
    {
        if (!_isRolling) return;

        var currentZRotation = transform.eulerAngles.z;
        var deltaZRotation = Mathf.DeltaAngle(previousZRotation, currentZRotation);
        cumulativeZRotation += deltaZRotation;
        previousZRotation = currentZRotation;

        var progress = cumulativeZRotation % angleThreshold / angleThreshold;

        progress = Mathf.Clamp01(progress);

        if (progress <= 0.5f)
        {
            targetScaleFactor = Mathf.Lerp(1f, 0.5f, progress * 2);
        }
        else
        {
            targetScaleFactor = Mathf.Lerp(0.5f, 1f, (progress - 0.5f) * 2);
        }

        arrowRectTransform.sizeDelta = Vector2.Lerp(arrowRectTransform.sizeDelta, originalSize * targetScaleFactor,
            Time.deltaTime * scaleSpeed);

        RollSound();
    }

    private void RollSound()
    {
        var epsilon = 0.2f;

        if (targetScaleFactor < 0.6f)
            _isTick = false;

        if (!(Mathf.Abs(targetScaleFactor - 1f) < epsilon) || _isTick) return;

        _isTick = true;
        RollSfx.Play();
    }

    private void AnimateTextChange(TextMeshProUGUI fading, TextMeshProUGUI dawning)
    {
        // if(dawning.IsActive()) return;
        //
        // fading.DOFade(0f, _changeTextDuration / 2f).OnComplete(() =>
        // {
        //     fading.gameObject.SetActive(false);
        // });
        //
        // dawning.gameObject.SetActive(true);
        // dawning.alpha = 0f;
        // dawning.DOFade(1f, _changeTextDuration / 2f);
    }

    private void EntrySkipRotationButton()
    {
        skipRotationButton.gameObject.SetActive(true);
        skipRotationButton.transform.DOScale(1, 0.3f);
        _tapToSkipRotationText.transform.DOScale(1, 0.3f);
    }

    private void HideSkipRotationButton()
    {
        var skipRotationButtonTransform = skipRotationButton.transform;
        if (skipRotationButtonTransform.localScale == Vector3.zero)
            return;

        skipRotationButtonTransform.DOScale(0, 0.3f);
        skipRotationButtonTransform.localScale = Vector3.zero;
        skipRotationButton.gameObject.SetActive(false);
        _tapToSkipRotationText.transform.DOScale(0, 0.3f);
        _tapToSkipRotationText.transform.localScale = Vector3.zero;
    }

    private void DarkSpinButton()
    {
        _spinButtonImage.color = new Color(0.6f, 0.6f, 0.6f, 1f);
        _spinText.color = new Color(0.6f, 0.6f, 0.6f, 1f);
    }
    
    public void LightSpinButton()
    {
        _spinButtonImage.color = new Color(1, 1, 1, 1f);
        _spinText.color = new Color(1, 1, 1, 1f);
    }
}