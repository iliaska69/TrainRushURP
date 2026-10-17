using DG.Tweening;
using UnityEngine;

public class EntryTrailerEngine : MonoBehaviour
{
    [Header("Left")] [SerializeField] private Transform leftSupportTransform;
    [SerializeField] private Transform leftEngineTransform;

    [Header("Right")] [SerializeField] private Transform rightSupportTransform;
    [SerializeField] private Transform rightEngineTransform;


    [Header("Duration")] [SerializeField] private float supportOpenDuration = 0.3f;
    [SerializeField] private float engineOpenDuration = 0.4f;
    [SerializeField] private float engineCloseDuration = 1f;

    [Header("Engine Particle")] [SerializeField]
    private ParticleSystem leftParticle;

    [SerializeField] private ParticleSystem rightParticle;

    private Trailer _trailer;

    private bool _isReadyToEntry;
    private int _trailersCount;

    private void Start()
    {
        _trailer = GetComponentInParent<Trailer>();
        ResetParam();
    }

    public void SetReadyToEntry(bool value)
    {
        _isReadyToEntry = value;
    }

    private void Update()
    {
        if (Game.instance.GetTrailerList().Count != _trailersCount)
            StartEntryAnimation();
    }

    public void StartEntryAnimation()
    {
        if (!_isReadyToEntry) return;

        var trailers = Game.instance.GetTrailerList();
        _trailersCount = trailers.Count;

        if (_trailer == null)
            return;

        var trailer = trailers.Find(t => t.gameObject.GetInstanceID() == _trailer.gameObject.GetInstanceID());
        if (trailer == null)
            return;

        EmitParticles();

        var sequence = DOTween.Sequence();
        sequence
            .Append(leftSupportTransform.DOScale(1, supportOpenDuration).SetEase(Ease.OutBack))
            .Join(rightSupportTransform.DOScale(1, supportOpenDuration).SetEase(Ease.OutBack))
            .Join(leftEngineTransform.DOScale(1, engineOpenDuration).SetEase(Ease.OutBack))
            .Join(rightEngineTransform.DOScale(1, engineOpenDuration).SetEase(Ease.OutBack));
    }

    public void CloseEngineAnimation()
    {
        var sequence = DOTween.Sequence();
        CloseFireParticles();

        sequence
            .Append(leftEngineTransform.DOScale(0, engineCloseDuration).SetEase(Ease.Linear))
            .Join(rightEngineTransform.DOScale(0, engineCloseDuration).SetEase(Ease.Linear))
            .Join(leftSupportTransform.DOScale(0, engineCloseDuration).SetEase(Ease.Linear))
            .Join(rightSupportTransform.DOScale(0, engineCloseDuration).SetEase(Ease.Linear));
    }

    private void EmitParticles()
    {
        leftParticle.gameObject.SetActive(true);
        leftParticle.Play();
        rightParticle.gameObject.SetActive(true);
        rightParticle.Play();
    }

    private void CloseFireParticles()
    {
        leftParticle.Pause();
        rightParticle.Pause();
        leftParticle.gameObject.SetActive(false);
        rightParticle.gameObject.SetActive(false);
    }

    private void ResetParam()
    {
        leftSupportTransform.transform.localScale = Vector3.zero;
        rightSupportTransform.transform.localScale = Vector3.zero;
        leftEngineTransform.transform.localScale = Vector3.zero;
        rightEngineTransform.transform.localScale = Vector3.zero;
        leftParticle.gameObject.SetActive(false);
        rightParticle.gameObject.SetActive(false);
    }
}