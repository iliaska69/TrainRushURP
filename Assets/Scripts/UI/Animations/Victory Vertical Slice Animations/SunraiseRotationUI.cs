using DG.Tweening;
using UnityEngine;

public class SunraiseRotationUI : MonoBehaviour
{
    [SerializeField] private float duration = 20f;
    
    private RectTransform _rectTransform; 
    
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        Rotation();
    }

    void Rotation()
    {
        _rectTransform.DORotate(new Vector3(0f, 0f, 360f), duration, RotateMode.LocalAxisAdd)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
    }
}
