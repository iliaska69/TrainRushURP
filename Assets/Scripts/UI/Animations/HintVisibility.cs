using UnityEngine;

public static class HintVisibility
{
    private static readonly TicketsManager TicketsManager = new();

    public static bool ShouldShow(HintVisibilitySource source)
    {
        return source switch
        {
            HintVisibilitySource.Tickets => TicketsManager.GetTickets() > 0,
            HintVisibilitySource.DailyReward => DailyRewardManager.instance != null
                && DailyRewardManager.instance.IsCanGetNextRewardAsync(),
            HintVisibilitySource.ShopAffordable => CanAffordUnpurchasedTrailer(),
            _ => false
        };
    }

    private static bool CanAffordUnpurchasedTrailer()
    {
        if (Shop.instance == null)
            return false;

        var balance = Bank.instance != null
            ? Bank.instance.GetBalance()
            : PlayerPrefs.GetInt(PlayerPrefsKeys.Balance, 0);

        foreach (var item in Shop.instance.GetUnpurchasedTrailers())
        {
            if (item.GetPrice() <= balance)
                return true;
        }

        return false;
    }
}
