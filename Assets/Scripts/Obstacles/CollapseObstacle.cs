using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    void Start()
    {
        CollapseFirstSphereAnimation();
        CollapseSecondSphereAnimation();
    }

    void CollapseFirstSphereAnimation()
    {
        if (!_isMovingFirst)
        {
            _isMovingFirst = true;

            firstHemisphere.DOLocalMoveX(firstTargetPosition, moveDuration)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => OnCollapseComplete());
        }
    }

    void CollapseSecondSphereAnimation()
    {
        if (!_isMovingSecond)
        {
            _isMovingSecond = true;

            secondHemisphere.DOLocalMoveX(secondTargetPosition, moveDuration)
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
        firstHemisphere.DOLocalMoveX(0f, moveDuration)
        .SetEase(Ease.InOutQuad)
        .OnComplete(() => OnCompleteReturnFirstAnimation());
    }

    void OnCompleteReturnFirstAnimation()
    {
        Invoke("CollapseFirstSphereAnimation", 3f);
    }

    void ReturnSecondAnimation()
    {
        secondHemisphere.DOLocalMoveX(0f, moveDuration)
        .SetEase(Ease.InOutQuad)
        .OnComplete(() => OnCompleteReturnSecondAnimation());

    }

    void OnCompleteReturnSecondAnimation()
    {
        Invoke("CollapseSecondSphereAnimation", 3f);
    }
}
