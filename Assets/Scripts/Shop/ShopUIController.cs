using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIController : MonoBehaviour
{
    [SerializeField] private GameObject spawnPlatform;

    private List<ShopItem> _shopItems = new List<ShopItem>();
    private ShopItem _currentUIItem;
    private Trailer _currentTrailerSkin;


    private void Update()
    {
            Debug.Log(_shopItems.Count);
        if(_shopItems == null)
        {
            _shopItems = Shop.instance.GetItems();
        }
    }

    void Start()
    {
        _currentTrailerSkin = Game.instance.GetTrailerSkin();
        foreach(var item in _shopItems)
        {
            if(_currentTrailerSkin == item.GetSkin())
            {
                _currentUIItem = item;
                Debug.Log(_currentTrailerSkin);
            }
        }
    }

}
