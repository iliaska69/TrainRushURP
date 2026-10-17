using UnityEngine;

[CreateAssetMenu(fileName = "Audio Dictionary", menuName = "ScriptableObjects/Audio Dictionary")]

public class AudioDictionarySO : ScriptableObject
{
    [field: SerializeField] public SerializableDictionary<AudioEnum, AudioClip>  AudioDictionary { get; private set; }
}
 