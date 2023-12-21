using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance = null;

    [SerializeField] private List<ShopItem> shopItems = new List<ShopItem>();
    [SerializeField] private ShopItem currentShopItem;

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
        shopItem.isBuy = true;
        shopItem.SetItemPurchased(true);
        Game.instance.SetTrailerSkin(shopItem);
        Bank.instance.SubstractMoney(price);

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
        currentShopItem= shopItem;
        PlayerPrefs.SetInt("CurrentShopItem", currentShopItem.GetInstanceID());
    }

    private void LoadCurrentShopItem()
    {
        var id = PlayerPrefs.GetInt("CurrentShopItem");
        
        foreach(var item in shopItems)
        {
            if(id == item.GetInstanceID())
            {
                currentShopItem = item;
            }
        }
        Game.instance.SetTrailerSkin(currentShopItem);
    }
}
