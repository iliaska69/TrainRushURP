using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class ShopPlatformSwitcher : MonoBehaviour
{
    [SerializeField] private IntEventChannelSO animationFinished;
    [SerializeField] private float arrivalDuration = 0.5f;
    [SerializeField] private float comeoutDuration = 0.5f;

    [SerializeField] private ShopPlatform _shopPlatformPref;
    [SerializeField] private Transform _spawnShopPlatformTransform;
    
    [SerializeField] private Transform leftSpawn;
    [SerializeField] private Transform rightSpawn;

    private Trailer _currentSkin;
    private Trailer _newSkin;
    private Vector3 _screenEnd;
    private bool isMoving = false;
    private Trailer _currentShopSkin;
    private ShopPlatform _shopPlatform;
    private ShopPlatform _newShopPlatform;

    private void Start()
    {
        _screenEnd = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, transform.position.z));
        _currentSkin = Game.instance.GetTrailerSkin();
        InstantiateTrailerPlatform();
    }

    public void ChangeShopItem(ShopItem item)
    {
        _currentSkin = item.GetSkin();
    }


    public void MovePlatform(int direction)
    {
        if (isMoving) return;
        isMoving = true;
        
        //спавним новую
        _newShopPlatform = Instantiate(
            _shopPlatformPref, 
            _screenEnd,
            _spawnShopPlatformTransform.rotation,
            _spawnShopPlatformTransform);

        var pos = new Vector3(
            rightSpawn.localPosition.x * -direction,
            rightSpawn.localPosition.y,
            rightSpawn.localPosition.z);
        _newShopPlatform.transform.localPosition = pos;
        
        
        _newShopPlatform.StopRotation();
        _currentShopSkin = Instantiate(_currentSkin, _newShopPlatform.GetSpawnTrailerPlace().position,
            _newShopPlatform.GetSpawnTrailerPlace().rotation, _newShopPlatform.transform);
        _currentShopSkin.GetTrailerMovementComponent().enabled = false;
        _currentShopSkin.GetCharacterAnim().gameObject.SetActive(false);
        
        //перемещаем новую
        _newShopPlatform.transform
            .DOMove(_spawnShopPlatformTransform.position, comeoutDuration)
            .SetEase(Ease.InOutQuart);
        
        //увозим старую
        _shopPlatform.StopRotation();

        var pos1 = new Vector3(
            rightSpawn.localPosition.x * direction,
            rightSpawn.localPosition.y,
            rightSpawn.localPosition.z);
        _shopPlatform.transform
            .DOLocalMove(pos1, comeoutDuration)
            .SetEase(Ease.InOutQuart)
            .OnComplete(() => OnMoveCurrentPlatformComplete());
    }

    void OnMoveCurrentPlatformComplete()
    {
        isMoving = false;
        Destroy(_shopPlatform.gameObject);
        _shopPlatform = _newShopPlatform;
        _shopPlatform.RotatePlatform();
        animationFinished.RaiseEvent(1);
    }

    void InstantiateTrailerPlatform()
    {
        _shopPlatform = Instantiate(_shopPlatformPref, _spawnShopPlatformTransform.position,
            _spawnShopPlatformTransform.rotation, _spawnShopPlatformTransform);

        _currentShopSkin = Instantiate(_currentSkin, _shopPlatform.GetSpawnTrailerPlace().position,
            _shopPlatform.GetSpawnTrailerPlace().rotation, _shopPlatform.transform);

        _currentShopSkin.GetTrailerMovementComponent().enabled = false;
        _currentShopSkin.GetCharacterAnim().gameObject.SetActive(false);
    }
}