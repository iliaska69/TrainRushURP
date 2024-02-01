using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WheelOfFortuneRollAnimation : MonoBehaviour
{
    [SerializeField] private int countOfRolls;
    [SerializeField] private VoidEventChannelSO finishRollAnimationEvenSO;

    int _angleOfReward;
    bool _isRolling;
    float _rotationZ;

    public void Roll(int winnerElementIndex, int countOfRewards)
    {
        winnerElementIndex++;
        if (_isRolling) return;

        _isRolling = true;
        _angleOfReward = 360 / countOfRewards;
        var fivePercentOfAngle = Mathf.RoundToInt(_angleOfReward * 0.05f);
        var lastRoll = _angleOfReward * winnerElementIndex - Random.Range(0 + fivePercentOfAngle, _angleOfReward - fivePercentOfAngle);
        _rotationZ = 360f * countOfRolls + lastRoll;

        transform.DORotate(new Vector3(0f, 0f, _rotationZ), countOfRolls * 2, RotateMode.FastBeyond360)
                .SetEase(Ease.OutCirc).OnComplete(() => OnCompleteRollAnimation());
    }

    private void OnCompleteRollAnimation()
    {
        _isRolling = false;
        finishRollAnimationEvenSO.RaiseEvent();
    }
}
