using UnityEngine;

public class WheelOfFortuneLevelUnlock : MonoBehaviour
{
    [SerializeField] private int unlockAfterCompletingLevel = 3;
    [SerializeField] private int starterTickets = 3;
    [SerializeField] private VoidEventChannelSO ticketChangedEvent;

    private readonly TicketsManager _ticketsManager = new();

    private void OnEnable()
    {
        TryGrantStarterTickets();
        RefreshVisibility();
    }

    public void RefreshVisibility()
    {
        gameObject.SetActive(IsUnlocked());
    }

    private void TryGrantStarterTickets()
    {
        if (!IsUnlocked())
            return;

        if (PlayerPrefs.GetInt(PlayerPrefsKeys.WheelUnlockStarterTicketsGranted, 0) == 1)
            return;

        PlayerPrefs.SetInt(PlayerPrefsKeys.WheelUnlockStarterTicketsGranted, 1);
        _ticketsManager.AddTickets(starterTickets);
        PlayerPrefs.Save();
        ticketChangedEvent?.RaiseEvent();
    }

    private bool IsUnlocked()
    {
        return GetCurrentLevelNumber() > unlockAfterCompletingLevel;
    }

    private static int GetCurrentLevelNumber()
    {
        if (LevelManager.instance != null)
            return LevelManager.instance.GetNumberOfLevel();

        var savedLevel = PlayerPrefs.GetInt(PlayerPrefsKeys.NumberOfLevel, 0);
        return savedLevel != 0 ? savedLevel : 1;
    }
}
