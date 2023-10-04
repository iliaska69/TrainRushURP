using TMPro;
using UnityEngine;

public class StatsShopUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMoneyBonus;

    
    public void ChangeTextMoneyBonus(ShopItem shopItem)
    {
        textMoneyBonus.text = shopItem.GetMoneyBonus().ToString();
    }
}
