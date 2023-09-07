using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Shop/Shop Item Event Channel")]
public class ShopItemEventChanelSO : SerializableBaseSO
{
    public UnityAction<ShopItem> OnEventRaised;
	
    public void RaiseEvent(ShopItem value)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(value);
        }
    }
}
