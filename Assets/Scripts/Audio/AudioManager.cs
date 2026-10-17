using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource additionalMusicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioDictionarySO audioDictionarySo;

    public bool IsMusicMuted { get; private set; }
    
    private const float defaultMusicVolume = 0.5f;
    private const string MusicMuteKey = "MuteMusic";
    
    private bool _canPlayMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            IsMusicMuted = PlayerPrefs.GetInt(MusicMuteKey, 0) == 1;
            musicSource.volume = defaultMusicVolume;
            musicSource.Stop();

            // if (IsMusicMuted)
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioSource PlayMusic(AudioEnum clipKey, float volume = defaultMusicVolume)
    {
        if (!audioDictionarySo.AudioDictionary.TryGetValue(clipKey, out var clip))
            return null;

        if (IsMusicMuted)
            return null;
        
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.volume = volume;
        musicSource.Play();
        
        return musicSource;
    }

    public AudioSource PlayAdditionalMusic(AudioEnum clipKey, float volume = defaultMusicVolume)
    {
        if (!audioDictionarySo.AudioDictionary.TryGetValue(clipKey, out var clip))
            return null;

        additionalMusicSource.clip = clip;
        additionalMusicSource.loop = true;
        additionalMusicSource.volume = volume;
        additionalMusicSource.Play();

        return additionalMusicSource;
    }

    public void MuteMusic()
    {
        musicSource.Stop();
        IsMusicMuted = true;
        PlayerPrefs.SetInt(MusicMuteKey, 1);
        PlayerPrefs.Save();
    }
    
    public void UnmuteMusic(AudioEnum clipKey)
    {
        IsMusicMuted = false;
        PlayerPrefs.SetInt(MusicMuteKey, 0);
        PlayerPrefs.Save();
        PlayMusic(clipKey);
    }

    public void PlaySfx(AudioEnum clipKey, float volume = 1f)
    {
        if (!audioDictionarySo.AudioDictionary.TryGetValue(clipKey, out var clip))
            return;

        sfxSource.PlayOneShot(clip, volume);
    }

    public AudioSource GetMainMusicAudioSource() => musicSource;
}