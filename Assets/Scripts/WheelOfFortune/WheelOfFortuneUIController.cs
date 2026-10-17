using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Rewards;

public class WheelOfFortuneUIController : MonoBehaviour
{
    [SerializeField] private List<Image> imagesOfRewards;
    [SerializeField] private WheelOfFortuneRollAnimation animationRollWheelOfFortune;
    [SerializeField] private ClickButtonAnimation buttonClickAnim;
    [SerializeField] private BackButtonWheelOfFortuneAnim backButtonWheelOfFortuneAnim;
    [SerializeField] private Image adImage;
    [SerializeField] private TextMeshProUGUI ticketCountText;
    [SerializeField] private VoidEventChannelSO getNewTicketEvent;
    [SerializeField] private UINavigator openWatchAdView;

    private Reward _winingReward;
    private List<Reward> _rewards = new();
    private TicketsManager _ticketsManager = new();
    private bool _isCanRoll = true;
    private int _winnerRewardIndex;


    private void Start()
    {
        _ticketsManager = new TicketsManager();
        Debug.Log("Count of tickets: " + _ticketsManager.GetTickets());
        Initialization();
        AdImageManageWithoutAnimation();
        RefreshTicketCount();
    }

    public void Initialization()
    {
        var rewards = CartWheelOfFortuneController.instance.GetListOfRewards();

        for (var i = 0; i < rewards.Count; i++)
        {
            _rewards.Add(rewards[i]);
            imagesOfRewards[i].sprite = _rewards[i].Sprite;
        }
    }

    public void RollWheel()
    {
        if (!_isCanRoll) return;

        if (_ticketsManager.GetTickets() <= 0)
        {
            openWatchAdView.OpenView();
            //WatchAd();
            
            return;
        }

        _ticketsManager.RemoveTicket();
        RefreshTicketCount();
        _isCanRoll = false;
        buttonClickAnim.OnButtonClick();
        backButtonWheelOfFortuneAnim.HideButton();

        _winingReward = CartWheelOfFortuneController.instance.GetNewWinningReward();

        _winnerRewardIndex = 0;

        for (var i = 0; i < _rewards.Count; i++)
        {
            if (_winingReward == _rewards[i])
            {
                _winnerRewardIndex = i;
                break;
            }
        }

        if (_winnerRewardIndex >= 0)
            animationRollWheelOfFortune.Roll(_winnerRewardIndex, _rewards.Count);
    }

    public void SetIsCanRoll(bool value)
    {
        _isCanRoll = value;
    }
    
    public void WinRewardAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(imagesOfRewards[_winnerRewardIndex].transform.DOScale(1.2f, 0.3f));
        sequence.SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

        DOVirtual.DelayedCall(1.5f, () =>
        {
            sequence.Kill();
            imagesOfRewards[_winnerRewardIndex].transform.DOScale(1f, 0.3f).SetEase(Ease.InOutSine);
        });
    }

    public void WatchAd()
    {
        Debug.Log("Watching ad");
        OnAdTicketRewardReceived();
    }

    private void OnAdTicketRewardReceived()
    {
        _ticketsManager.AddTickets(1);
        RefreshTicketCount();
        AdImageManageWithoutAnimation();
        getNewTicketEvent?.RaiseEvent();
    }

    public void RefreshTicketCount()
    {
        if (ticketCountText == null) return;

        ticketCountText.text = _ticketsManager.GetTickets().ToString();
    }

    private void AdImageManageWithAnimation()
    {
        if (_ticketsManager.GetTickets() <= 0)
        {
            adImage.gameObject.SetActive(true);
            adImage.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        }
        else
        {
            adImage.gameObject.SetActive(false);
            adImage.transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        }
    }

    private void AdImageManageWithoutAnimation()
    {
        adImage.gameObject.SetActive(_ticketsManager.GetTickets() <= 0);
    }
}