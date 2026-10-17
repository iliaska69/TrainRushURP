using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardUI : MonoBehaviour
{
    [SerializeField] private Image mainImage;
    [SerializeField] private Image activateCheckMark;
    [SerializeField] private Image activateFade;
    [SerializeField] private Image sun;
    [SerializeField] private GameObject stars;
    
    //[SerializeField] private Image rewardImage;
    [SerializeField] private float animationDuration = 0.5f;

    public void ActivateRewardAnimation()
    {
        ActivateCheckMarkAnimation();
        ActivateFadeAnimation();
        DeactivateCurrentRewardAnimation();
    }

    public void SetRewardImages(Image newImage)
    {
        mainImage.sprite = newImage.sprite;
    }

    public void ActivateCurrentRewardAnimation()
    {
        sun.transform.localScale = Vector3.one;
        stars.SetActive(true);
    }
    
    public void DeactivateCurrentRewardAnimation()
    {
        stars.SetActive(false);
        sun.transform.localScale = Vector3.zero;
    }

    public void UnusedReward()
    {
        activateCheckMark.transform.localScale = Vector3.zero;
        activateCheckMark.color =
            new Color(activateCheckMark.color.r, activateCheckMark.color.g, activateCheckMark.color.b, 0);
        activateFade.color = 
            new Color(activateFade.color.r, activateFade.color.g, activateFade.color.b, 0);
        sun.transform.localScale = Vector3.zero;
        stars.SetActive(false);
    }

    public Image GetMainImage() => mainImage;
    
    private void ActivateCheckMarkAnimation()
    {
        activateCheckMark.transform.DOScale(Vector3.one, animationDuration).SetEase(Ease.OutBack);
        activateCheckMark.DOFade(1f, animationDuration).SetEase(Ease.Linear);
    }

    private void ActivateFadeAnimation()
    {
        activateFade.DOFade(1f, animationDuration).SetEase(Ease.Linear);
    }

    
}