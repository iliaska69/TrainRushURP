using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class ShopPlatform : MonoBehaviour
{
    [SerializeField] private float rotationDuration = 15f;
    [SerializeField] private Transform spawnPlace;
    [SerializeField] private IntEventChannelSO animationFinished;
    [SerializeField] private float arrivalDuration = 0.5f;
    [SerializeField] private float comeoutDuration = 0.5f;

    private Trailer _currentSkin;
    private Trailer _currentShopSkin;
    private Vector3 _screenEnd;
    private bool isMoving = false;
    private bool isRotating = false;


    private void Start()
    {
        _screenEnd = Camera.main.ScreenToWorldPoint(
           new Vector3(Screen.width, Screen.height, transform.position.z));
        _currentSkin = Game.instance.GetTrailerSkin();
        InstantiateTrailer();
        RotatePlatform();
    }

    public void ChangeShopItem(ShopItem item)
    {
        _currentSkin = item.GetSkin();
    }

    public void MovePlatform(int direction)
    {
        if (!isMoving)
        {
            isMoving = true;
            StopRotation();
            transform.DOMoveX(direction * _screenEnd.x * 2f + direction * transform.localScale.x, comeoutDuration)
                .SetEase(Ease.InOutQuart)
                .OnComplete(() => OnMoveComplete(direction));
        }
    }

    void InstantiateTrailer()
    {
        _currentShopSkin = Instantiate(_currentSkin, spawnPlace.position, spawnPlace.rotation, spawnPlace);
        _currentShopSkin.GetTrailerMovementComponent().enabled = false;
    }
                                                                                                                                                                                                                                                        
    void RotatePlatform()
    {
        isRotating = true;
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        transform.DORotate(new Vector3(0f, 360f, 0f), rotationDuration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear)
            .OnKill(() => isRotating = false)
            .SetSpeedBased(true)
            .OnPlay(() => transform.rotation = Quaternion.identity);
    }

    void OnMoveComplete(int direction)
    {
        direction = direction > 0 ? -1 : 1;

        transform.position = new Vector3(
           direction * _screenEnd.x * 2f + direction * transform.localScale.x,
           transform.position.y,
           transform.position.z);

        Destroy(_currentShopSkin.gameObject);
        InstantiateTrailer();
        RotatePlatform();

        transform.DOMoveX(0f, arrivalDuration)
            .SetEase(Ease.InOutQuart)
            .OnComplete(() => OnMoveAnimationComplete());
    }

    void OnMoveAnimationComplete()
    {
        isMoving = false;
        animationFinished.RaiseEvent(1);
    }

    void StopRotation()
    {
        if (isRotating)
        {
            transform.DOKill();
            isRotating = false;
        }
    }
}
