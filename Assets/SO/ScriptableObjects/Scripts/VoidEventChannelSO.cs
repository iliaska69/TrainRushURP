using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have но arguments.
/// Example: Exit Game Event.
/// </summary>

[CreateAssetMenu(menuName = "Events/Void Event Channel")]
public class VoidEventChannelSO : DescriptionBaseSO
{
    public UnityAction OnEventRaised;

    public void RaiseEvent()
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke();
    }
}