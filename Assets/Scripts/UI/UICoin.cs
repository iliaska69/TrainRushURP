using UnityEngine;

public class UICoin : MonoBehaviour
{
    [SerializeField] private CollectingCoin _collectingCoin;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var collectingCoin = other.GetComponent<Coin>();
        if (collectingCoin == null)
            return;
        
        _collectingCoin.CollideWithUICoin();
    }
}
