using UnityEngine;

public class CircleCoinEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem punchStarsParticleSystems;

    public void Play()
    {
        if (punchStarsParticleSystems == null) return;
        punchStarsParticleSystems.Emit(1);
    }
}
