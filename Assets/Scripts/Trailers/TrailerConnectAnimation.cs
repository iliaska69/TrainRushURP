using DG.Tweening;
using UnityEngine;

public class TrailerConnectAnimation : MonoBehaviour
{
    [SerializeField] private GameObject trailer;
    [SerializeField] private GameObject character;
    [SerializeField] private float punchDuration;
    [SerializeField] private float scale;
    
    public void BounceAnimation()
    {
        trailer.transform.DOPunchScale(new Vector3(scale, scale, scale), punchDuration).SetEase(Ease.OutQuart);
        character.transform.DOPunchScale(new Vector3(scale, scale, scale), punchDuration).SetEase(Ease.OutQuart);
    }
}
