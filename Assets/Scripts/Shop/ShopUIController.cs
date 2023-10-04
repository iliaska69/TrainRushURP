using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopUIController : MonoBehaviour
{
    [SerializeField] private GameObject leftButton;
    [SerializeField] private GameObject rightButton;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject setButton;

    [SerializeField] private ShopItemEventChanelSO shopItemChanged;

    private List<ShopItem> _shopItems = new List<ShopItem>();
    private ShopItem _currentUIItem;
    private int _currentShopItemIndex;
    private ShopItem _currentTrailerSkin;


    void Start()
    {
        _shopItems = Shop.instance.GetItems();
        _currentTrailerSkin = Shop.instance.GetCurrentShopItem();
        for (var i = 0; i < _shopItems.Count; i++)
        {
            if (_currentTrailerSkin == _shopItems[i])
            {
                _currentUIItem = _shopItems[i];
                _currentShopItemIndex = i;
            }
        }
        if (_currentUIItem == _shopItems.First())
        {
            leftButton.SetActive(false);
        }

        if (_currentShopItemIndex == _shopItems.Count - 1) rightButton.SetActive(false);
        if (_currentShopItemIndex == 0) leftButton.SetActive(false);

        BuySetButtonsManager();
        shopItemChanged.RaiseEvent(_currentUIItem);
    }

    //Листаем машинки вправо
    public void GetNextSkin()
    {
        if (_currentShopItemIndex == _shopItems.Count - 1) return;

        _currentShopItemIndex++;
        if (_currentShopItemIndex == _shopItems.Count - 1) rightButton.SetActive(false);
       
        _currentUIItem = _shopItems[_currentShopItemIndex];

        shopItemChanged.RaiseEvent(_currentUIItem);

        if (!leftButton.activeSelf) leftButton.SetActive(true);

        BuySetButtonsManager();
    }

    //Листаем машинки влево
    public void GetPreviousSkin()
    {
        if (_currentShopItemIndex <= 0) return;
        
        _currentShopItemIndex--;
        if(_currentShopItemIndex == 0) leftButton.SetActive(false);

        _currentUIItem = _shopItems[_currentShopItemIndex];

        shopItemChanged.RaiseEvent(_currentUIItem);

        if (!rightButton.activeSelf) rightButton.SetActive(true);

        BuySetButtonsManager();
    }

    public void BuyItem()
    {
        if (_currentUIItem.AlreadyBuy()) return;
        var checkPurchase = Shop.instance.Buy(_currentUIItem);

        if (!checkPurchase) return;
        buyButton.SetActive(false);
        SetItem();
    }

    public void SetItem()
    {
        if (!_currentUIItem.AlreadyBuy()) return;
        Game.instance.SetTrailerSkin(_currentUIItem);
        Shop.instance.SetCurrentShopItem(_currentUIItem);
        _currentTrailerSkin = _currentUIItem;
        setButton.SetActive(false);
    }

    private void BuySetButtonsManager()
    {
        if (!_currentUIItem.AlreadyBuy())
        {
            buyButton.SetActive(true);
            setButton.SetActive(false);
        }
        else
        {
            buyButton.SetActive(false);
            if (_currentUIItem != Shop.instance.GetCurrentShopItem())
            {
                setButton.SetActive(true);
            }
            else
            {
                setButton.SetActive(false);
            }
        }
    }
}