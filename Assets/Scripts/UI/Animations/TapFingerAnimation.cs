using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class TapFingerAnimation : MonoBehaviour
{
    [SerializeField] private bool playOnEnable = true;
    [SerializeField] private HintVisibilitySource visibilitySource = HintVisibilitySource.Tickets;
    [SerializeField] private VoidEventChannelSO visibilityChangedEvent;

    [Header("Tap")]
    [SerializeField] private Vector2 tapOffset = new(0f, -35f);
    [SerializeField] private float tapScale = 0.85f;
    [SerializeField] private float tapRotation = 8f;

    [Header("Timing")]
    [SerializeField] private float tapDownDuration = 0.12f;
    [SerializeField] private float tapHoldDuration = 0.05f;
    [SerializeField] private float tapUpDuration = 0.18f;
    [SerializeField] private float pauseBetweenTaps = 0.6f;

    [Header("Ease")]
    [SerializeField] private Ease tapDownEase = Ease.InQuad;
    [SerializeField] private Ease tapUpEase = Ease.OutBack;

    private RectTransform _rectTransform;
    private Sequence _tapSequence;
    private Vector2 _startAnchoredPosition;
    private Vector3 _startScale;
    private Quaternion _startRotation;
    private void Awake()
    {
        _rectTransform = (RectTransform)transform;
    }

    private void OnEnable()
    {
        CacheStartValues();

        if (visibilityChangedEvent != null)
            visibilityChangedEvent.OnEventRaised += RefreshVisibility;

        RefreshVisibility();
    }

    private void OnDisable()
    {
        if (visibilityChangedEvent != null)
            visibilityChangedEvent.OnEventRaised -= RefreshVisibility;

        Stop();
    }

    public void RefreshVisibility()
    {
        if (HintVisibility.ShouldShow(visibilitySource))
        {
            _rectTransform.localScale = _startScale;

            if (playOnEnable)
                Play();
        }
        else
        {
            Stop();
            _rectTransform.localScale = Vector3.zero;
        }
    }

    public void Play()
    {
        Stop();
        CacheStartValues();

        var tapPosition = _startAnchoredPosition + tapOffset;
        var pressedScale = _startScale * tapScale;

        _tapSequence = DOTween.Sequence()
            .Append(_rectTransform.DOAnchorPos(tapPosition, tapDownDuration).SetEase(tapDownEase))
            .Join(_rectTransform.DOScale(pressedScale, tapDownDuration).SetEase(tapDownEase))
            .Join(_rectTransform.DORotate(new Vector3(0f, 0f, tapRotation), tapDownDuration, RotateMode.LocalAxisAdd).SetEase(tapDownEase))
            .AppendInterval(tapHoldDuration)
            .Append(_rectTransform.DOAnchorPos(_startAnchoredPosition, tapUpDuration).SetEase(tapUpEase))
            .Join(_rectTransform.DOScale(_startScale, tapUpDuration).SetEase(tapUpEase))
            .Join(_rectTransform.DORotate(new Vector3(0f, 0f, -tapRotation), tapUpDuration, RotateMode.LocalAxisAdd).SetEase(tapUpEase))
            .AppendInterval(pauseBetweenTaps)
            .SetLoops(-1)
            .SetUpdate(true);
    }

    public void Stop()
    {
        _tapSequence?.Kill();
        _tapSequence = null;

        if (_rectTransform == null)
            return;

        _rectTransform.DOKill();
        RestoreStartValues();
    }

    private void CacheStartValues()
    {
        _startAnchoredPosition = _rectTransform.anchoredPosition;
        _startScale = _rectTransform.localScale;
        _startRotation = _rectTransform.localRotation;
    }

    private void RestoreStartValues()
    {
        _rectTransform.anchoredPosition = _startAnchoredPosition;
        _rectTransform.localScale = _startScale;
        _rectTransform.localRotation = _startRotation;
    }
}
