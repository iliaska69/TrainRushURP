using UnityEngine;

[CreateAssetMenu(fileName = "Shop Item", menuName = "ScriptableObjects/Shop Item", order = 2)]
public class ShopItem : ScriptableObject
{
    [SerializeField] private int price;
    [SerializeField] private Trailer skin;
    [SerializeField] private float speed;
    [SerializeField] private bool isBuy;

    public int GetPrice()
    {
        return price;
    }

    public Trailer GetSkin()
    {
        return skin;
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
