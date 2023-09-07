using UnityEngine;

public class ShopPlatform : MonoBehaviour
{
    [SerializeField] private Transform spawnPlace;

    private Trailer _currentSkin;
    private Trailer _currentShopSkin;

    private void Start()
    {
        _currentSkin = Game.instance.GetTrailerSkin();
        _currentShopSkin = Instantiate(_currentSkin, spawnPlace, false);
    }

    public void ChangeShopItem(ShopItem item)
    {
        Destroy(_currentShopSkin.gameObject);
        _currentSkin= item.GetSkin();
        _currentShopSkin = Instantiate(_currentSkin, spawnPlace, false);
    }
}
