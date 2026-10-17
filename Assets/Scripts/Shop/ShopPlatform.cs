using DG.Tweening;
using UnityEngine;

public class ShopPlatform : MonoBehaviour
{
    [SerializeField] private float rotationDuration = 15f;
    [SerializeField] private Transform spawnTrailerPlace;
    
    private bool isRotating = false;
    private ShopPlatform _newShopPlatform;

    private void Start()
    {
        RotatePlatform();
    }
                                                                                                                                                                                                                                                        
    public void RotatePlatform()
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
    
    public void StopRotation()
    {
        if (isRotating)
        {
            transform.DOKill();
            isRotating = false;
        }
    }

    public Transform GetSpawnTrailerPlace() => spawnTrailerPlace;
}
