using DG.Tweening;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleTenAnimation : MonoBehaviour
{
    [SerializeField] private Transform[] pathPoints;
    [SerializeField] private float pathDuration;
    [SerializeField] private float firstPathRotationZ;
    [SerializeField] private float secondPathRotationZ;
    [SerializeField] private RotateMode rotateMode;

    private Vector3[] firstPathVectors;
    private Vector3[] secondPathVectors;
    private Transform _transform;

    private void Start()
    {
        _transform = transform;
        firstPathVectors = new Vector3[pathPoints.Length];
        firstPathVectors = GetFirstPathVector3Array();
        secondPathVectors = new Vector3[pathPoints.Length];
        secondPathVectors = firstPathVectors.Reverse().ToArray();

        FirstMoveOnPath();
    }

    private void FirstMoveOnPath()
    {
        _transform.DOPath(firstPathVectors, pathDuration, PathType.CatmullRom)
            .SetOptions(false)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() => SecondMoveOnPath());

        _transform.DORotate(new Vector3(0, 0, firstPathRotationZ), pathDuration, rotateMode)
            .SetEase(Ease.InOutQuart);
    }

    private void SecondMoveOnPath()
    {
        _transform.DOPath(secondPathVectors, pathDuration, PathType.CatmullRom)
            .SetOptions(false)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() => Invoke("FirstMoveOnPath", 2f));

        _transform.DORotate(new Vector3(0, 0, secondPathRotationZ), pathDuration, rotateMode)
            .SetEase(Ease.InOutQuart);
    }

    private Vector3[] GetFirstPathVector3Array()
    {
        Vector3[] path = new Vector3[pathPoints.Length];
        for (int i = 0; i < pathPoints.Length; i++)
        {
            path[i] = pathPoints[i].position;
        }
        return path;
    }
}
