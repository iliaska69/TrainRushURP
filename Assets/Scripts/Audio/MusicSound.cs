using UnityEngine;

public class MusicSound : MonoBehaviour
{
    [SerializeField] private AudioEnum key;
    [SerializeField] private float volume = 1f;
    
    [field: SerializeField] public float Duration { get; set; }

    public AudioSource AudioSource { get; private set; }

    private void Start()
    {
        AudioSource = AudioManager.Instance.GetMainMusicAudioSource();
    }

    public void Play()
    {
        AudioManager.Instance.PlayMusic(key, volume);
    }

    public void Stop()
    {
        AudioSource.Stop();
    }
}
