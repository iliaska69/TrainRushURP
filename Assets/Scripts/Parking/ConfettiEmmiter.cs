using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ConfettiEmmiter : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> particles;
    [SerializeField] private List<ParticleSystem> cameraConfetti;
    [SerializeField] private float spawnInterval;
    [SerializeField] private SfxSound confettiPoofSfx;

    public void StartConfettiPerformance()
    {
        if (particles.Count % 2 != 0)
        {
            Debug.LogError("The number of particles must be even!");
            return;
        }
        SpawnParticlesInPairsAsync().Forget();
    }
    
    private async UniTaskVoid SpawnParticlesInPairsAsync()
    {
        for (var i = 0; i < particles.Count; i += 2)
        {
            particles[i].Emit(50);
            particles[i + 1].Emit(50);
            confettiPoofSfx.Play();
            
            await UniTask.Delay((int)(spawnInterval * 1000));
        }

        foreach (var confetti in cameraConfetti)
        {
            confetti.Emit(30);
        }
        confettiPoofSfx.Play();
    }
}
