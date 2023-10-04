using UnityEngine;

[CreateAssetMenu(fileName = "Shop Item", menuName = "ScriptableObjects/Shop Item", order = 2)]
public class ShopItem : ScriptableObject
{
    public bool isBuy;
    [SerializeField] private int price;
    [SerializeField] private Trailer skin;
    [SerializeField] private float speed;
    [SerializeField] private int moneyBonus;

    public float GetSpeed()
    {
        return speed;
    }

    public int GetPrice()
    {
        return price;
    }

    public Trailer GetSkin()
    {
        return skin;
    }

    public int GetMoneyBonus()
    {
        return moneyBonus;
    }

    public bool AlreadyBuy()
    {
        return isBuy;
    }

    public void SetItemPurchased(bool value)
    {
        isBuy = value;
    }
}
