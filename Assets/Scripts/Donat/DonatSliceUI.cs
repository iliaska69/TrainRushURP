using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DonatSliceUI : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> prices;
    
    private void Start()
    {
        var products = Purchaser.Instance.GetCoinsPrices();
        for (int i = 0; i < products.Count; i++)
        {
            prices[i].text = products[i].metadata.localizedPriceString;
        }
    }

    public void DonatButtonOneOnClick(TextMeshProUGUI countOfCoins)
    {
        var cleaned = countOfCoins.text.Replace("$", "").Trim();

        if (!int.TryParse(cleaned, out var price)) return;
        
        Purchaser.Instance.BuyCoins(price);
    }
}