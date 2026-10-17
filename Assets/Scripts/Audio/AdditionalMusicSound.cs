using UnityEngine;

public class AdditionalMusicSound : MonoBehaviour
{
    [SerializeField] private AudioEnum key;
    [SerializeField] private float volume = 1f;

    [field: SerializeField] public float Duration { get; set; }

    public AudioSource Play()
    {
        return AudioManager.Instance.PlayAdditionalMusic(key, volume);
    }
}