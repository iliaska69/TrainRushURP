using UnityEngine;

public class CoinEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem punchStarsParticleSystems;


    public void PlayPunchStarsEffect()
    {
        if (punchStarsParticleSystems == null) return;
        punchStarsParticleSystems.Emit(2);
    }
}
