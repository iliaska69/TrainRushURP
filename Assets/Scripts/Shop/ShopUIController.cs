using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
    private Trailer _currentTrailerSkin;


    void Start()
    {
        _shopItems = Shop.instance.GetItems();
        _currentTrailerSkin = Game.instance.GetTrailerSkin();
        for (var i = 0; i < _shopItems.Count; i++)
        {
            if (_currentTrailerSkin == _shopItems[i].GetSkin())
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

        buyButton.SetActive(false);
        setButton.SetActive(false);
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

        BuyButtonManager();
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

        BuyButtonManager();
    }

    public void BuyItem()
    {
        if (_currentUIItem.AlreadyBuy()) return;
        Shop.instance.Buy(_currentUIItem);
    }

    public void SetItem()
    {
        if (!_currentUIItem.AlreadyBuy()) return;
        Game.instance.SetTrailerSkin(_currentTrailerSkin);
    }

    private void BuyButtonManager()
    {
        if (!_currentUIItem.AlreadyBuy())
        {
            buyButton.SetActive(true);
            setButton.SetActive(false);
        }
        else
        {
            buyButton.SetActive(false);
            setButton.SetActive(true);
        }
    }
}