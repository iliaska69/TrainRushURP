using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance = null;

    [SerializeField] private List<ShopItem> shopItems = new List<ShopItem>();
    [SerializeField] private ShopItem currentShopItem;
    [SerializeField] private VoidEventChannelSO shopAffordableChangedEvent;

    private List<int> _shopItemsBuyState = new();
    private const string BOUGTH_SHOPITEM = "BuyShopItem";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
        
        //Delete
        // PlayerPrefs.DeleteKey("CurrentShopItem");
        // PlayerPrefs.SetInt("CurrentShopItem", currentShopItem.GetInstanceID());
        // for (var i = 0; i < shopItems.Count; i++)
        // {
        //     PlayerPrefs.SetInt(BOUGTH_SHOPITEM + i, 0);
        // }
        
        
        LoadBoughtItems();
        LoadCurrentShopItem();
    }

    public bool Buy(ShopItem shopItem)
    {
        var price = shopItem.GetPrice();

        var isEnoughMoney = Bank.instance.CheckBalance(price);
        if (!isEnoughMoney)
        {
            Debug.Log("Not enough money");
            return false;
        }

        for (int i = 0; i < shopItems.Count; i++)
        {
            if (shopItem != shopItems[i]) continue;
            PlayerPrefs.SetInt(BOUGTH_SHOPITEM + i, 1);
        }
        
        shopItem.isBuy = true;
        
        Game.instance.SetTrailerSkin(shopItem);
        Bank.instance.SubstractBalance(price);
        NotifyShopAffordableChanged();

        return true;
    }

    public List<ShopItem> GetItems()
    {
        return shopItems;
    }

    public ShopItem GetCurrentShopItem()
    {
        return currentShopItem;
    }

    public void SetCurrentShopItem(ShopItem shopItem)
    {
        currentShopItem = shopItem;
        PlayerPrefs.SetInt("CurrentShopItem", currentShopItem.GetInstanceID());
    }

    public List<ShopItem> GetUnpurchasedTrailers()
    {
        List<ShopItem> unperchaseTrailers = new();
        foreach (var shopItem in shopItems)
        {
            if (shopItem.isBuy) continue;

            unperchaseTrailers.Add(shopItem);
        }

        return unperchaseTrailers;
    }

    public void SetShopItemIsBuy(ShopItem shopItem)
    {
        for (int i = 0; i < shopItems.Count; i++)
        {
            if (shopItem != shopItems[i]) continue;
            PlayerPrefs.SetInt(BOUGTH_SHOPITEM + i, 1);
        }
        
        shopItem.isBuy = true;
        NotifyShopAffordableChanged();
    }

    private void NotifyShopAffordableChanged()
    {
        shopAffordableChangedEvent?.RaiseEvent();
    }
    
    private void LoadCurrentShopItem()
    {
        var id = PlayerPrefs.GetInt("CurrentShopItem");

        foreach (var item in shopItems)
        {
            if (id == item.GetInstanceID())
            {
                currentShopItem = item;
            }
        }

        Game.instance.SetTrailerSkin(currentShopItem);
    }

    private void LoadBoughtItems()
    {
        PlayerPrefs.SetInt(BOUGTH_SHOPITEM + 0, 1);

        for (var i = 0; i < shopItems.Count; i++)
        {
            var value = PlayerPrefs.GetInt(BOUGTH_SHOPITEM + i);
            shopItems[i].isBuy = Convert.ToBoolean(value);
            //shopItems[i].isBuy = true;
        }
    }
}