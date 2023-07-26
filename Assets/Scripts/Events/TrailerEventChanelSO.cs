using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Trailers/Trailer Event Channel")]
public class TrailerEventChanelSO : SerializableBaseSO
{
    public UnityAction<Trailer> OnEventRaised;
	
    public void RaiseEvent(Trailer value)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(value);
        }
    }
}
