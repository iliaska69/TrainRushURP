using UnityEngine;

public class WatchAdVictoryUIController : MonoBehaviour
{
    [SerializeField] private UINavigator uiNavigator;
    [SerializeField] private VoidEventChannelSO nextLevelUploadEvent;

    public void WatchAd()
    {
        //тут посмотреть рекламу
        Debug.LogWarning("[WatchAdVictory] Reward ad is not assigned.");
        GiveMultiplyReward();
    }

    public void GiveMultiplyReward()
    {
        WinManager.ApplyCachedMultiplyCoins();
        UINewRandomTrailer.ApplyPendingTrailerIfAny();
        nextLevelUploadEvent?.RaiseEvent();
        uiNavigator?.OpenView();
    }
}
