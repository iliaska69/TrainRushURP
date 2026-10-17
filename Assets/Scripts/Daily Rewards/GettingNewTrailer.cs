using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GettingNewTrailer : MonoBehaviour
{
    [SerializeField] private Image trailerImage;
    
    //private List<ShopItem> _unperchaseTrailers = new();

    private ShopItem _randomShopItem;
    
    void Start()
    {
        GetRandomTrailer();
        SetRandomTrailer();
    }
    
    public void GetTrailerButtonOnClick()
    {
        _randomShopItem.isBuy = true;
        Shop.instance.SetShopItemIsBuy(_randomShopItem);
        Shop.instance.SetCurrentShopItem(_randomShopItem);
        
        Game.instance.SetTrailerSkin(_randomShopItem);
        Game.instance.ClearTrailerList();
        
        LevelManager.instance.ReloadLevel();
    }
    
    private void GetRandomTrailer()
    {
        var shopItems = Shop.instance.GetUnpurchasedTrailers();
        if (shopItems.Count == 0) return;
        var randomItem = Random.Range(0, shopItems.Count);
        _randomShopItem = shopItems[randomItem];
    }

    private void SetRandomTrailer()
    {
        trailerImage.sprite = _randomShopItem.trailerUiImage;
    }
}
