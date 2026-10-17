using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollectibleCoinVfx : MonoBehaviour
{
    [SerializeField] private ParticleSystem levitationParticle;
    [SerializeField] private ParticleSystem burstParticle;
    [SerializeField] private GameObject levitationRoot;
    [SerializeField] private GameObject burstRoot;
    [SerializeField] private Collider pickupCollider;
    [SerializeField] private bool destroyAfterCollect = true;
    [SerializeField] private int coinValue = 100;
    [SerializeField] private IntEventChannelSO collectedEvent;
    [SerializeField] private float coinSfxVolume = 5f;

    private bool _isCollected;

    private void Awake()
    {
        if (pickupCollider == null)
            pickupCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isCollected)
            return;

        var trailer = other.GetComponentInParent<Trailer>();
        if (trailer == null || !trailer.IsMainTrailer())
            return;

        Collect();
        
    }

    [Button]
    public void Play()
    {
        burstParticle.Play();
    }
    public void Collect()
    {
        if (_isCollected)
            return;

        _isCollected = true;

        if (pickupCollider != null)
            pickupCollider.enabled = false;

        StopLevitation();
        burstParticle.Play();

        AudioManager.Instance?.PlaySfx(AudioEnum.Coin, coinSfxVolume);

        if (Bank.instance != null)
            Bank.instance.AddCoins(coinValue);

        collectedEvent?.RaiseEvent(coinValue);

        if (!destroyAfterCollect)
            return;

        var destroyDelay = 2f;
        if (burstParticle != null)
        {
            var main = burstParticle.main;
            destroyDelay = main.duration + main.startLifetime.constantMax;
        }

        Destroy(gameObject, destroyDelay);
    }

    private void StopLevitation()
    {
        if (levitationParticle != null)
            levitationParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        var target = levitationRoot != null ? levitationRoot : levitationParticle?.gameObject;
        if (target == null || target == gameObject)
            return;

        target.SetActive(false);
    }
}
