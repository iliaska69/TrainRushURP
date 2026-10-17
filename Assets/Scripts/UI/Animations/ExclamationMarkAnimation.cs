using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ExclamationMarkAnimation : MonoBehaviour
{
    [SerializeField] private bool playOnEnable = true;
    [SerializeField] private HintVisibilitySource visibilitySource = HintVisibilitySource.Tickets;
    [SerializeField] private VoidEventChannelSO visibilityChangedEvent;

    [Header("Punch")]
    [SerializeField] private Vector3 punchScale = new(0.35f, 0.35f, 0f);
    [SerializeField] private Vector2 punchAnchor = new(-18f, 0f);
    [SerializeField] private int punchVibrato = 8;
    [SerializeField] private float punchElasticity = 0.6f;

    [Header("Timing")]
    [SerializeField] private float punchDuration = 0.45f;
    [SerializeField] private float pauseBetweenPunches = 0.75f;

    private RectTransform _rectTransform;
    private Sequence _punchSequence;
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

        _punchSequence = DOTween.Sequence()
            .Append(_rectTransform.DOPunchScale(punchScale, punchDuration, punchVibrato, punchElasticity))
            .Join(_rectTransform.DOPunchAnchorPos(punchAnchor, punchDuration, punchVibrato, punchElasticity))
            .AppendInterval(pauseBetweenPunches)
            .SetLoops(-1)
            .SetUpdate(true);
    }

    public void Stop()
    {
        _punchSequence?.Kill();
        _punchSequence = null;

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
