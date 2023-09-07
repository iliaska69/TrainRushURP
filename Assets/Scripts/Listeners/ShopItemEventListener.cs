using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ShopItemEvent : UnityEvent<ShopItem>
{

}

public class ShopItemEventListener : MonoBehaviour
{
    [SerializeField] private ShopItemEventChanelSO _channel;

    public ShopItemEvent OnEventRaised;

    private void OnEnable()
    {
        if (_channel != null)
            _channel.OnEventRaised += Respond;
    }

    private void OnDisable()
    {
        if (_channel != null)
            _channel.OnEventRaised -= Respond;
    }

    private void Respond(ShopItem value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}
