using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class PlayerPrefsKeys
{
    public const string Balance = "Balance";
    public const string CountOfLevelCoins = "Count of new level coins";
    public const string CountOfNewCoins = "Count of new coins";
    public const string CountOfTickets = "Count of tickets";
    public const string WheelUnlockStarterTicketsGranted = "Wheel unlock starter tickets granted";

    public const string RewardIndex = "Reward index";
    public const string LastDay = "Last day";

    public const string GameIsDone = "Game is done";
    public const string LastLevelIndexes = "List of last level indexes";
    public const string NumberOfLevel = "Number of level";

    public const string SelectedLanguage = "SELECTED_LANGUAGE";
    public const string PercentOfNewTrailer = "Percent of new trailer";

    public const string MuteMusic = "MuteMusic";
    public const string IsInterstitialTurnedOff = "IsInterstitialTurnedOff";
    public const string IsBannerTurnedOff = "IsBannerTurnedOff";
    public const string NoAdsHidden = "NoAdsHidden";

    public const string CurrentShopItem = "CurrentShopItem";

    private const string BuyShopItemPrefix = "BuyShopItem";
    public const int MaxShopItems = 32;

    public static IEnumerable<string> GetAllKeys(int maxShopItems = MaxShopItems)
    {
        foreach (var field in typeof(PlayerPrefsKeys).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            if (field.FieldType != typeof(string) || !field.IsLiteral)
                continue;

            yield return (string)field.GetRawConstantValue();
        }

        for (var i = 0; i < maxShopItems; i++)
            yield return BuyShopItemPrefix + i;
    }

    public static void DeleteAll(int maxShopItems = MaxShopItems)
    {
        foreach (var key in GetAllKeys(maxShopItems))
            PlayerPrefs.DeleteKey(key);

        PlayerPrefs.Save();
        Debug.Log("[ResetSaves] PlayerPrefs cleared.");
    }
}
