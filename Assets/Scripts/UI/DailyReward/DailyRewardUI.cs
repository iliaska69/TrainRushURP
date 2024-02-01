using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardUI : MonoBehaviour
{
    [SerializeField] private List<Image> dailyRewardImages;
    [SerializeField] private GameObject lockOpenButton;
    [SerializeField] private GameObject lockCloseButton;
    [SerializeField] private GameObject usedGiftImage;


    void Start()
    {
        ManageDailyRewardImages();
        ManageClaimButton();
    }

    private void ManageDailyRewardImages()
    {
        var dailyRewards = DailyRewardManager.instance.GetDailyrewards();
        for (var i = 0; i < dailyRewardImages.Count; i++)
        {
            dailyRewardImages[i].sprite = dailyRewards[i].rewardSprite;
        }
    }

    private void ManageClaimButton()
    {
        if (DailyRewardManager.instance.IsCanGetNextReward())
        {
            lockOpenButton.SetActive(true);
            lockCloseButton.SetActive(false);
        }
        else
        {
            lockOpenButton.SetActive(false);
            lockCloseButton.SetActive(true);
        }
    }

    public void OnClickClaim()
    {
        if (DailyRewardManager.instance.IsCanGetNextReward())
        {
            DailyRewardManager.instance.ActivateReward();
            ManageClaimButton();
        }
    }
}
