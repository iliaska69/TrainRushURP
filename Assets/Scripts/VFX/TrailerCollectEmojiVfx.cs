using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class TrailerCollectEmojiVfx : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void Play()
    {
        if (_particleSystem == null)
            return;

        _particleSystem.Play();
    }
}
