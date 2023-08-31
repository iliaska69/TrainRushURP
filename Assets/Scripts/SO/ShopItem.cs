using UnityEngine;

[CreateAssetMenu(fileName = "Shop Item", menuName = "ScriptableObjects/Shop Item", order = 2)]
public class ShopItem : ScriptableObject
{
    [SerializeField] private int price;
    [SerializeField] private Trailer skin;
    [SerializeField] private float speed;

    public int GetPrice()
    {
        return price;
    }

    public Trailer GetSkin()
    {
        return skin;
    }
}
