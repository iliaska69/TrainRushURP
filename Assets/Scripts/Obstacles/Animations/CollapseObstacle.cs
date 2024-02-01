using DG.Tweening;
using UnityEngine;

public class CollapseObstacle : MonoBehaviour
{
    [SerializeField] private Transform firstHemisphere;
    [SerializeField] private Transform secondHemisphere;
    [SerializeField] private float firstTargetPosition;
    [SerializeField] private float secondTargetPosition;
    [SerializeField] private float moveDuration;

    private bool _isMovingFirst;
    private bool _isMovingSecond;
    private float _startPositionXFirstHemisphere;
    private float _startPositionXSecondHemisphere;

    private Transform _firstHemisphere;
    private Transform _secondHemisphere;

    void Start()
    {
        _firstHemisphere = firstHemisphere;
        _secondHemisphere = secondHemisphere;
        _startPositionXFirstHemisphere = firstHemisphere.position.x;
        _startPositionXSecondHemisphere = secondHemisphere.position.x;
        CollapseFirstSphereAnimation();
        CollapseSecondSphereAnimation();
    }

    void CollapseFirstSphereAnimation()
    {
        if (!_isMovingFirst)
        {
            _isMovingFirst = true;

            _firstHemisphere.DOLocalMoveX(firstTargetPosition, moveDuration)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => OnCollapseComplete());
        }
    }

    void CollapseSecondSphereAnimation()
    {
        if (!_isMovingSecond)
        {
            _isMovingSecond = true;

            _secondHemisphere.DOLocalMoveX(secondTargetPosition, moveDuration)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => OnCollapseComplete());
        }
    }

    void OnCollapseComplete()
    {
        _isMovingFirst = false;
        _isMovingSecond = false;

        Invoke("ReturnFirstAnimation", 1f);
        Invoke("ReturnSecondAnimation", 1f);
    }

    void ReturnFirstAnimation()
    {
        _firstHemisphere.DOMoveX(_startPositionXFirstHemisphere, moveDuration)
        .SetEase(Ease.InOutQuad)
        .OnComplete(() => OnCompleteReturnFirstAnimation());
    }

    void OnCompleteReturnFirstAnimation()
    {
        Invoke("CollapseFirstSphereAnimation", 3f);
    }

    void ReturnSecondAnimation()
    {
        _secondHemisphere.DOMoveX(_startPositionXSecondHemisphere, moveDuration)
        .SetEase(Ease.InOutQuad)
        .OnComplete(() => OnCompleteReturnSecondAnimation());

    }

    void OnCompleteReturnSecondAnimation()
    {
        Invoke("CollapseSecondSphereAnimation", 3f);
    }
}
