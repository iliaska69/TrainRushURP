using DG.Tweening;
using UnityEngine;

public class ClickButtonAnimation : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float putValue;
    [SerializeField] private float backValue;
    [SerializeField] private SfxSound sfxSound;
    
    private Transform buttonTransform;
    private Tweener scaleTween;
    private bool _isLock;

    private void Start()
    {
        buttonTransform = gameObject.GetComponent<Transform>();
        backValue = gameObject.transform.localScale.x;
    }

    public void OnButtonClick()
    {
        scaleTween?.Kill();
        sfxSound.Play();
        buttonTransform = gameObject.GetComponent<Transform>();
        scaleTween = buttonTransform.DOScale(putValue, duration/2).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            buttonTransform.DOScale(backValue, duration);
        });
    }
}
