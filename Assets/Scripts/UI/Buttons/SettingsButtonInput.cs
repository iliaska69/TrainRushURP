using System;
using DG.Tweening;
using UnityEngine;

public class SettingsButtonInput : MonoBehaviour
{
    [SerializeField] private Transform gearTransform;
    [SerializeField] private Transform settingCoverTransform;
    [SerializeField] private SfxSound sfxSound;


    [SerializeField] private float moveDown;
    [SerializeField] private float moveUp;
    [SerializeField] private float returnDuration;


    private bool _buttonUsed;
    private GameObject _settingsSlice;
    private bool _isAnimating;


    public void ClickSettingsButton()
    {
        if (_isAnimating) return;
        sfxSound.Play();

        RotateGear(_buttonUsed);
        SettingsMovement(_buttonUsed);

        if (_buttonUsed)
            _buttonUsed = false;
        else
            _buttonUsed = true;
    }

    private void RotateGear(bool buttonUsed)
    {
        if (buttonUsed)
            gearTransform.DORotate(new Vector3(0, 0, 0), 0.5f);
        else
            gearTransform.DORotate(new Vector3(0, 0, -90), 0.5f);
    }

    private void SettingsMovement(bool buttonUsed)
    {
        _isAnimating = true;

        if (buttonUsed)
        {
            settingCoverTransform.DOLocalMoveY(383, moveUp)
                .OnKill(() => _isAnimating = false);
        }
        else
        {
            var sequence = DOTween.Sequence();

            sequence.Append(settingCoverTransform.DOLocalMoveY(-104f, moveDown))
                .Append(settingCoverTransform.DOLocalMoveY(-30f, returnDuration)
                    .SetEase(Ease.InOutBack))
                .OnKill(() => _isAnimating = false);
        }
    }
}