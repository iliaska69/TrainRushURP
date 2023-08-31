using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance = null;

    [SerializeField] private List<ShopItem> shopItems = new List<ShopItem>();

    private void Awake()
    {
        Debug.Log("PISKA");
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
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

        Game.instance.SetTrailerSkin(shopItem.GetSkin());
        Bank.instance.SubstractMoney(price);

        return true;
    }

    public List<ShopItem> GetItems()
    {
        return shopItems;
    }
}
