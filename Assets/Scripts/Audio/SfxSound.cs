using UnityEngine;

public class SfxSound : MonoBehaviour
{
    [SerializeField] private AudioEnum key;
    [SerializeField] private float volume = 1f;
    
    [field: SerializeField] public float Duration { get; set; }
    
    public void Play()
    {
        AudioManager.Instance.PlaySfx(key, volume);
    }
}
