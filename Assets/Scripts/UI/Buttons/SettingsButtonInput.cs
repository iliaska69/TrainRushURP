using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SettingsButtonInput : MonoBehaviour
{
    [SerializeField] private Transform gearTransform;
    [SerializeField] private Transform settingCoverTransform;

    private bool _buttonUsed;
    private GameObject _settingsSlice;


    public void ClickSettingsButton()
    {
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
        if (buttonUsed)
        {
            settingCoverTransform.DOLocalMoveY(557, 0.5f);
        }
        else
        {
            var sequence = DOTween.Sequence();
            sequence.Append(
                settingCoverTransform.DOLocalMoveY(-40f, 0.5f));

            sequence.Append(
                settingCoverTransform
                    .DOLocalMoveY(53f, 0.5f)
                    .SetEase(Ease.InOutBack));
        }
            
    }
}
