using DG.Tweening;
using UnityEngine;

public class EngineBonusAnimation : MonoBehaviour
{
    [Header("Levitation Settings")]
    [SerializeField] private float levitationHeight = 0.5f;
    [SerializeField] private float levitationDuration = 2f;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 360f;

    [SerializeField] private Transform rotationObjectTransform;
    private Transform _transform;
    private void Start()
    {
        _transform = transform;
        StartLevitation();
        StartRotation();
    }
    
    private void StartLevitation()
    {
        _transform.DOLocalMoveY(_transform.localPosition.y + levitationHeight, levitationDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void StartRotation()
    {
        var currentRotation = transform.localEulerAngles;
        var targetRotation = currentRotation + new Vector3(0, 360, 0);

        transform.DOLocalRotate(targetRotation, rotationSpeed, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental); // Бесконечное вращение с накоплением
    }
}
