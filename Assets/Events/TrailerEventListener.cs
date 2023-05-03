using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrailerEventListener : MonoBehaviour
{
    [SerializeField] private TrailerEventChanelSO _channel;

    public UnityEvent<Trailer> OnEventRaised;

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

    private void Respond(Trailer value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}
