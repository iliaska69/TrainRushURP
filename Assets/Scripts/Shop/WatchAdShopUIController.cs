using UnityEngine;

public class WatchAdShopUIController : MonoBehaviour
{
    [SerializeField] private UINavigator uiNavigator;
    [SerializeField] private VoidEventChannelSO coinsReceivedEvent;

    private const int RewardCoins = 100;

    public void WatchAd()
    {
        //тут посмотреть рекламу
        Debug.LogWarning("[WatchAdShop] Reward ad is not assigned.");
        GiveCoins();
    }

    public void GiveCoins()
    {
        Bank.instance.AddClearBalance(RewardCoins);
        coinsReceivedEvent?.RaiseEvent();
        uiNavigator?.OpenView();
    }
}
