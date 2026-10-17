using UnityEngine;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance { get; private set; }

    [SerializeField] private int countOfLevelsForInterstitialAd = 2;
    [SerializeField] private int countOfLevelsNoAd = 6;
    
    private int _currentLevelCounter;
    private int _sinceLastAdCounter;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void IncrementCountOfLevels()
    {
        _currentLevelCounter++;
        _sinceLastAdCounter++;

        // if (_currentLevelCounter <= countOfLevelsNoAd)
        //     return;

        if (_sinceLastAdCounter <= countOfLevelsForInterstitialAd) return;

        ResetCurrentLevelCounter();
    }

    public void ResetCurrentLevelCounter()
    {
        _sinceLastAdCounter = 0;
    }
}