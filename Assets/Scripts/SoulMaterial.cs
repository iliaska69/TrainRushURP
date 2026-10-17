using System.Collections.Generic;
using UnityEngine;

public class SoulMaterial : MonoBehaviour
{
    [SerializeField] private List<Material> soulMaterials;

    void Start()
    {
        if (soulMaterials == null || soulMaterials.Count == 0)
        {
            Debug.LogWarning("SoulMaterial: No materials assigned!");
            return;
        }

        if (TryGetComponent(out ParticleSystem particleSystem) &&
            TryGetComponent(out ParticleSystemRenderer psr))
        {
            var trails = particleSystem.trails;
            trails.enabled = true;
            psr.trailMaterial = soulMaterials[Random.Range(0, soulMaterials.Count)];
        }
        else
        {
            Debug.LogWarning("SoulMaterial: ParticleSystem or ParticleSystemRenderer missing!");
        }
    }
}
