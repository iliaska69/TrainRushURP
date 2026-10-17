using DG.Tweening;
using UnityEngine;

public class ObstacleSevenAnimation : MonoBehaviour
{
    [SerializeField] private float rotationDuration;
    [SerializeField] private float delayBetweenAnimations;
    [SerializeField] private float startRotationZ;
    [SerializeField] private float targetRotationZ;
    [SerializeField] private float startDelay;


    void Start()
    {
        Invoke(nameof(RotateToTarget), startDelay);
    }

    void RotateToTarget()
    {
        transform.DOLocalRotate(new Vector3(0, 0, targetRotationZ), rotationDuration)
            .SetEase(Ease.InOutQuart)
            .OnComplete(() => Invoke("RotateBack", delayBetweenAnimations));
    }

    private void RotateBack()
    {
        transform.DOLocalRotate(new Vector3(0, 0, startRotationZ), rotationDuration)
            .SetEase(Ease.InOutQuart)
            .OnComplete(() => Invoke("RotateToTarget", 0f));
    }
}
