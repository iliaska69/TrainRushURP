using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TrailerEvent : UnityEvent<Trailer>
{

}

public class TrailerEventListener : MonoBehaviour
{
    [SerializeField] private TrailerEventChanelSO _channel;

    public TrailerEvent OnEventRaised;

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
