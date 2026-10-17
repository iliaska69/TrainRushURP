using UnityEngine;

public class WatchAdDefeatUIController : MonoBehaviour
{
    [SerializeField] private UINavigator uiNavigator;
    [SerializeField] private VoidEventChannelSO activateTrailersVoidEventChannelSo;

    public void WatchAd()
    {
        //тут посмотреть рекламу
        Debug.LogWarning("[WatchAdDefeat] Reward ad is not assigned.");
        Revive();
    }

    public void Revive()
    {
        activateTrailersVoidEventChannelSo?.RaiseEvent();
        uiNavigator?.OpenView();
    }
}
