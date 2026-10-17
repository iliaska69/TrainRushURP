using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopUIController : MonoBehaviour
{
    [Header("Arrows")]
    [SerializeField] private GameObject leftButton;
    [SerializeField] private GameObject rightButton;

    [Header("Buy button")]
    [SerializeField] private GameObject buyButton;

    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI costTextShadow;

    [Header("Set button")]
    [SerializeField] private GameObject setButton;

    [SerializeField] private Image checkMark;
    [SerializeField] private TextMeshProUGUI setText;
    [SerializeField] private TextMeshProUGUI setTextShadow;

    [Header("Stats")]
    [SerializeField] private List<GameObject> coinStats;
    [SerializeField] private List<GameObject> speedStats;

    [Header("Events")]
    [SerializeField] private ShopItemEventChanelSO shopItemChanged;
    [SerializeField] private IntEventChannelSO shopArrowOnClicked;
    
    
    [SerializeField] private SimpleClickButtonAnimation buySimpleClickButtonAnimation;
    [SerializeField] private SimpleClickButtonAnimation setSimpleClickButtonAnimation;
    [SerializeField] private UINavigator openWatchAdView;


    private List<ShopItem> _shopItems = new List<ShopItem>();
    private ShopItem _currentShopItem;
    private int _currentShopItemIndex;
    private ShopItem _currentTrailerSkin;
    private bool _isCanGetNewSkin = true;
    private bool _isCheckMark;


    void Start()
    {
        _shopItems = Shop.instance.GetItems();
        _currentTrailerSkin = Shop.instance.GetCurrentShopItem();
        _isCheckMark = true;
        for (var i = 0; i < _shopItems.Count; i++)
        {
            if (_currentTrailerSkin == _shopItems[i])
            {
                _currentShopItem = _shopItems[i];
                _currentShopItemIndex = i;
            }
        }

        if (_currentShopItem == _shopItems.First())
        {
            leftButton.SetActive(false);
        }

        if (_currentShopItemIndex == _shopItems.Count - 1) rightButton.SetActive(false);
        if (_currentShopItemIndex == 0) leftButton.SetActive(false);

        BuySetButtonsManager();
        ShopItemStatsManage();
        shopItemChanged.RaiseEvent(_currentShopItem);
    }

    //������� ������� ������
    public void GetNextSkin()
    {
        if (!_isCanGetNewSkin) return;
        _isCanGetNewSkin = false;

        if (_currentShopItemIndex == _shopItems.Count - 1) return;

        _currentShopItemIndex++;
        if (_currentShopItemIndex == _shopItems.Count - 1) rightButton.SetActive(false);

        _currentShopItem = _shopItems[_currentShopItemIndex];
        shopItemChanged.RaiseEvent(_currentShopItem);
        shopArrowOnClicked.RaiseEvent(-1);

        if (!leftButton.activeSelf) leftButton.SetActive(true);

        BuySetButtonsManager();
        ShopItemStatsManage();
    }

    //������� ������� �����
    public void GetPreviousSkin()
    {
        if (!_isCanGetNewSkin) return;
        _isCanGetNewSkin = false;

        if (_currentShopItemIndex <= 0) return;

        _currentShopItemIndex--;
        if (_currentShopItemIndex == 0) leftButton.SetActive(false);

        _currentShopItem = _shopItems[_currentShopItemIndex];

        shopItemChanged.RaiseEvent(_currentShopItem);
        shopArrowOnClicked.RaiseEvent(1);

        if (!rightButton.activeSelf) rightButton.SetActive(true);

        BuySetButtonsManager();
        ShopItemStatsManage();
    }

    public void OpenWatchAdView()
    {
        openWatchAdView?.OpenView();
    }

    public void BuyItem()
    {
        if (_currentShopItem.AlreadyBuy()) return;
        
        var checkPurchase = Shop.instance.Buy(_currentShopItem);
        
        if (!checkPurchase) return;
        buyButton.SetActive(false);
        SetItem();
        SetButtons();
        BuyTrailer();
        buySimpleClickButtonAnimation.OnButtonClick();
    }

    private void SetItem()
    {
        if (!_currentShopItem.AlreadyBuy()) return;
        Game.instance.SetTrailerSkin(_currentShopItem);
        Shop.instance.SetCurrentShopItem(_currentShopItem);
        _currentTrailerSkin = _currentShopItem;
        setButton.SetActive(false);
    }

    public void CanGetSkin(int value)
    {
        _isCanGetNewSkin = value != 0;
    }

    public void SetItemClick()
    {
        if (_isCheckMark) return;
        setSimpleClickButtonAnimation.OnButtonClick();
        SetButtons();
    }
    
    public void SetButtons()
    {
        Shop.instance.SetCurrentShopItem(_currentShopItem);
        Game.instance.SetTrailerSkin(_currentShopItem);
        if (_isCheckMark) return;

        checkMark.transform.localScale = Vector3.zero;
        checkMark.color =
            new Color(checkMark.color.r, checkMark.color.g, checkMark.color.b, 0);
        
        checkMark.gameObject.SetActive(true);
        checkMark.transform.DOScale(Vector3.one, 0.6f).SetEase(Ease.OutBack);
        checkMark.DOFade(1f,  0.6f).SetEase(Ease.Linear);
        
        setText.gameObject.SetActive(false);
        setTextShadow.gameObject.SetActive(false);
        _isCheckMark = true;
    }

    public void BuyTrailer()
    {
        _currentShopItem.isBuy = true;
        buyButton.SetActive(false);
        setText.gameObject.SetActive(false);
        setTextShadow.gameObject.SetActive(false);
        checkMark.transform.localScale = Vector3.zero;
        checkMark.color =
            new Color(checkMark.color.r, checkMark.color.g, checkMark.color.b, 0);
        setButton.SetActive(true);
        checkMark.gameObject.SetActive(true);
        checkMark.transform.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutBack);
        checkMark.DOFade(1f,  0.8f).SetEase(Ease.Linear);
    }
    
    private void BuySetButtonsManager()
    {
        if (!_currentShopItem.AlreadyBuy())
        {
            setButton.SetActive(false);
            buyButton.SetActive(true);
            costText.text = _currentShopItem.GetPrice().ToString();
            costTextShadow.text = _currentShopItem.GetPrice().ToString();
            _isCheckMark = false;
        }
        else
        {
            setButton.SetActive(true);
            if (Game.instance.GetTrailerSkin() == _currentShopItem.GetSkin())
            {
                checkMark.gameObject.SetActive(true);
                _isCheckMark = true;
                setText.gameObject.SetActive(false);
                setTextShadow.gameObject.SetActive(false);
            }
            else
            {
                checkMark.gameObject.SetActive(false);
                _isCheckMark = false;
                setText.gameObject.SetActive(true);
                setTextShadow.gameObject.SetActive(true);
            }

            buyButton.SetActive(false);
        }
    }

    private void ShopItemStatsManage()
    {
        var countOfCoinBonus = _currentShopItem.GetMoneyBonus();
        for (var i = 0; i < coinStats.Count; i++)
        {
            if (i < countOfCoinBonus)
                coinStats[i].SetActive(true);
            else
                coinStats[i].SetActive(false);
        }

        var countOfSpeedBonus = _currentShopItem.GetSpeed();
        for (var i = 0; i < speedStats.Count; i++)
        {
            if (i < countOfSpeedBonus)
                speedStats[i].SetActive(true);
            else
                speedStats[i].SetActive(false);
        }
    }
}