using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TimeAnimation : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private VoidEventChannelSO restartLevelVoidEventChannelSo;
    [SerializeField] private UINavigator uiNavigator;
    
    private Image _fillImage;
    private bool coolingDown = true;
    private bool _isUsed;


    private void Start()
    {
        _fillImage = GetComponent<Image>();
    }

    private void Update()
    {
        if (coolingDown)
        {
            _fillImage.fillAmount -= 1.0f / time * Time.deltaTime;
        }

        if (_fillImage.fillAmount <= 0f && !_isUsed)
        {
            _isUsed = true;
            restartLevelVoidEventChannelSo.RaiseEvent();
            uiNavigator.OpenView();
        }
    }
}