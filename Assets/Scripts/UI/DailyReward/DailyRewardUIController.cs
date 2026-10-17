using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardUIController : MonoBehaviour
{
    [SerializeField] private List<GameObject> rewardGameObjects;
    [SerializeField] private GameObject rewardParent;
    [SerializeField] private Button claimButton;
    [SerializeField] private TextMeshProUGUI claimText;

    [SerializeField] private VoidEventChannelSO openWelcomeViewEvent;
    [SerializeField] private VoidEventChannelSO openNewTrailerViewEvent;
    [SerializeField] private VoidEventChannelSO openWheelOfFortuneViewEvent;
    [SerializeField] private VoidEventChannelSO dailyRewardStateChangedEvent;

    private int _currentRewardIndex;
    private bool _isRewardUsed;
    private bool _isCanGetNextReward;
    private List<DailyRewardSO> _dailyRewardSOs;
    private List<DailyRewardUI> dailyRewardUIs = new();
    private Image _claimButtonImage;

    private bool _canClickClaim = true;
    private bool _hasInternet;

    private void Awake()
    {
        _claimButtonImage = claimButton.GetComponent<Image>();
    }

    void Start()
    {
        Instantiate();
        ManageClaimButton();
        ManageDailyRewardImages();
    }
    
    public async void OnClickClaim()
    {
        if (!_canClickClaim) return;
        _canClickClaim = false;

        DailyRewardManager.instance.ActivateReward();
        dailyRewardUIs[_currentRewardIndex].ActivateRewardAnimation();
        AnimateDisableButton();
        dailyRewardStateChangedEvent?.RaiseEvent();

        await UniTask.Delay(TimeSpan.FromSeconds(0.8f));

        if (_currentRewardIndex == dailyRewardUIs.Count - 1)
        {
            if (Shop.instance.GetUnpurchasedTrailers().Count == 0)
            {
                openWelcomeViewEvent.RaiseEvent();
                return;
            }

            openNewTrailerViewEvent.RaiseEvent();
        }
        else if (new TicketsManager().GetTickets() > 0)
        {
            openWheelOfFortuneViewEvent.RaiseEvent();
        }
        else
            openWelcomeViewEvent.RaiseEvent();
    }

    private void ManageDailyRewardImages()
    {
        for (var i = 0; i < dailyRewardUIs.Count; i++)
        {
            if (i < _currentRewardIndex)
            {
                dailyRewardUIs[i].DeactivateCurrentRewardAnimation();
            }

            if (i >= _currentRewardIndex)
            {
                dailyRewardUIs[i].UnusedReward();
            }

            if (i == _currentRewardIndex && _isCanGetNextReward)
            {
                dailyRewardUIs[i].ActivateCurrentRewardAnimation();
            }
        }
    }

    private void Instantiate()
    {
        var isHorizontalResolution = UIController.instance.IsHorizontal();
        _dailyRewardSOs = DailyRewardManager.instance.GetDailyRewards();

        if (rewardGameObjects == null || rewardGameObjects.Count < _dailyRewardSOs.Count)
        {
            Debug.LogError(
                $"DailyRewardUIController: rewardGameObjects ({rewardGameObjects?.Count ?? 0}) " +
                $"must match daily rewards count ({_dailyRewardSOs.Count}). Check prefab references.");
            return;
        }

        for (var i = 0; i < _dailyRewardSOs.Count; i++)
        {
            var dailyRewardUI = Instantiate(_dailyRewardSOs[i].dailyRewardUI, rewardParent.transform);
            dailyRewardUIs.Add(dailyRewardUI);
            dailyRewardUIs[i].transform.localPosition = rewardGameObjects[i].transform.localPosition;
            if (isHorizontalResolution)
            {
                dailyRewardUIs[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }

        for (var i = 0; i < rewardGameObjects.Count; i++)
        {
            Destroy(rewardGameObjects[i].gameObject);
        }

        rewardGameObjects.Clear();
        _currentRewardIndex = DailyRewardManager.instance.GetCurrentRewardIndex();
        _isCanGetNextReward = DailyRewardManager.instance.IsCanGetNextRewardAsync();
    }

    private async void ManageClaimButton()
    {
        bool canInteract = _isCanGetNextReward;

        claimButton.enabled = canInteract;
        Color color = canInteract ? Color.white : new Color(0.6f, 0.6f, 0.6f);
        _claimButtonImage.color = color;
        claimText.color = color;
    }

    private void AnimateDisableButton()
    {
        claimButton.enabled = false;
        Color targetColor = new Color(0.6f, 0.6f, 0.6f, 1f);

        _claimButtonImage.DOColor(targetColor, 0.5f);
        claimText.DOColor(targetColor, 0.5f);
    }
}