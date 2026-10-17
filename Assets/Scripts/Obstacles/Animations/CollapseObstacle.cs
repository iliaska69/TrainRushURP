using System;
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
    [SerializeField] private float startDelay;
    [SerializeField] private float delayBetweenAnim;

    private bool _isMovingFirst;
    private bool _isMovingSecond;
    private float _startPositionXFirstHemisphere;
    private float _startPositionXSecondHemisphere;

    void Start()
    {
        _startPositionXFirstHemisphere = firstHemisphere.localPosition.x;
        _startPositionXSecondHemisphere = secondHemisphere.localPosition.x;
        Invoke(nameof(CollapseFirstSphereAnimation), startDelay);
        Invoke(nameof(CollapseSecondSphereAnimation), startDelay);
        // CollapseFirstSphereAnimation();
        // CollapseSecondSphereAnimation();
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

        Invoke("ReturnFirstAnimation", 0.5f);
        Invoke("ReturnSecondAnimation", 0.5f);
    }

    void ReturnFirstAnimation()
    {
        firstHemisphere.DOLocalMoveX(_startPositionXFirstHemisphere, moveDuration)
        .SetEase(Ease.InOutQuad)
        .OnComplete(() => OnCompleteReturnFirstAnimation());
    }

    void OnCompleteReturnFirstAnimation()
    {
        Invoke("CollapseFirstSphereAnimation", delayBetweenAnim);
    }

    void ReturnSecondAnimation()
    {
        secondHemisphere.DOLocalMoveX(_startPositionXSecondHemisphere, moveDuration)
        .SetEase(Ease.InOutQuad)
        .OnComplete(() => OnCompleteReturnSecondAnimation());

    }

    void OnCompleteReturnSecondAnimation()
    {
        Invoke("CollapseSecondSphereAnimation", delayBetweenAnim);
    }
}
