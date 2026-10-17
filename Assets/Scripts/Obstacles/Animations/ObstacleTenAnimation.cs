using DG.Tweening;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class ObstacleTenAnimation : MonoBehaviour
{
    [SerializeField] private Transform[] pathPoints;
    [SerializeField] private float pathDuration = 2f;
    [SerializeField] private float rotationDuration = 2f;
    [SerializeField] private float interval = 2f;
    [SerializeField] private float startDelay;

    private Vector3[] firstPathVectors;
    private Vector3[] secondPathVectors;

    private void Start()
    {
        transform.position = pathPoints[0].position;
        firstPathVectors = pathPoints.Select(p => p.position).ToArray();
        secondPathVectors = firstPathVectors.Reverse().ToArray();
        
        Invoke(nameof(StartAnimationLoop), startDelay);
    }

    private void StartAnimationLoop()
    {
        var seq = DOTween.Sequence();

        // Первая часть: движение вперёд + вращение
        seq.Append(transform.DOPath(firstPathVectors, pathDuration, PathType.CatmullRom)
            .SetEase(Ease.InOutCubic));
        seq.Join(transform.DORotate(new Vector3(0, 0, -179), rotationDuration, RotateMode.LocalAxisAdd)
            .SetEase(Ease.InOutQuart));

        seq.AppendInterval(interval);
        // Вторая часть: движение назад + вращение
        seq.Append(transform.DOPath(secondPathVectors, pathDuration, PathType.CatmullRom)
            .SetEase(Ease.InOutCubic));
        seq.Join(transform.DORotate(new Vector3(0, 0, 179), rotationDuration, RotateMode.LocalAxisAdd)
            .SetEase(Ease.InOutQuart));

        seq.AppendInterval(interval);
        seq.SetLoops(-1);
    }
}