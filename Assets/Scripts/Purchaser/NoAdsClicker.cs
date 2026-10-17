using UnityEngine;

public class NoAdsClicker : MonoBehaviour
{
   public void NoAdsOnClick()
   {
      Purchaser.Instance.BuyRemoveAds();
   }
}
