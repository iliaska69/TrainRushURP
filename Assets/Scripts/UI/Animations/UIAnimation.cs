using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    [SerializeField] protected float openDuration;
    [SerializeField] protected float closeDuration;

    protected bool _isOpenAnimation;
    public float GetOpenDuration() => openDuration;
    public float GetCloseDuration() => closeDuration;


    public virtual void OpenAnimation(float openDuration)
    {

    }

    public virtual void CloseAnimation(float closeDuration)
    {

    }
}
