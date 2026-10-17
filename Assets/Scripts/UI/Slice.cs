using UnityEngine;

public class Slice : MonoBehaviour
{
    [SerializeField] private UIAnimation sliceAnimation;
    [SerializeField] private Canvas canvas;

    private Camera _uiCamera;

    
    public void PlayCloseAnimations()
    {
        if (sliceAnimation == null) return;

        sliceAnimation.CloseAnimation(sliceAnimation.GetCloseDuration());
    }

    public float GetCloseAnimationDuration()
    {
        if (sliceAnimation == null) return 0;

        return sliceAnimation.GetCloseDuration();
    }

    public void SetUICamera(Camera uiCamera)
    {
        _uiCamera = uiCamera;
        canvas.worldCamera = _uiCamera;
    }
}