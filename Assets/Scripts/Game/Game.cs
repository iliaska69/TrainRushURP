using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game instance;

    [SerializeField] private VoidEventChannelSO defeatEventChannel;
    [SerializeField] private VoidEventChannelSO victoryEventChannel;
    [SerializeField] private VoidEventChannelSO finishEventChannel;
    [SerializeField] private VoidEventChannelSO finishPerformanceEventChannel;

    [Header("Sounds")] [SerializeField] private SfxSound defeatSfx;
    [SerializeField] private SfxSound victorySfx;
    [SerializeField] private MusicSound mainMusic;

    private Trailer trailerSkin;
    private List<Trailer> trailers = new();
    private int _countOfFinishedTrailers;
    private bool _isVictory;
    private int _moneyBonusFromTrailer;
    private Railway _railway;
    private int _countOfWonLevelCoins;
    private Trailer _mainTrailer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }


    public void VictoryGame()
    {
        if (_isVictory) return;
        CountWonMoney();
        LevelManager.instance.IncrementNumberOnLevel();
        NewRandomTrailerController.instance.IncrementPercentOfNewRandomTrailer(_countOfFinishedTrailers);
        StartCoroutine(VictoryGameCoroutine());
    }

    public void SetIsVictory(bool value)
    {
        _isVictory = value;
    }

    public void IncrementCountOfFinishedTrailer()
    {
        _countOfFinishedTrailers++;

        if (_countOfFinishedTrailers != trailers.Count) return;

        VictoryGame();
        finishEventChannel.RaiseEvent();
    }

    public void FreezeGame()
    {
        Time.timeScale = 0f;
    }

    public void UnfreezeGame()
    {
        Time.timeScale = 1f;
    }

    public void IncrementCountOfTrailers(Trailer trailer)
    {
        trailers.Add(trailer);
    }

    public void DecrementCountOfTrailers(Trailer trailer)
    {
        trailers.Remove(trailer);
        _railway.DeleteKeyFromDictionaries(trailer.gameObject.GetInstanceID());
        if (trailers.Count == 0)
            Defeat();
    }

    public Trailer GetLastTrailer()
    {
        if (trailers.Count == 0) return null;
        return trailers[0];
    }

    public void ClearTrailerList()
    {
        trailers.Clear();
        _countOfFinishedTrailers = 0;
    }

    public List<Trailer> GetTrailerList()
        => trailers;

    public Trailer GetTrailerSkin()
    {
        return trailerSkin;
    }

    public void SetTrailerSkin(ShopItem shopItem)
    {
        trailerSkin = shopItem.GetSkin();
        SetMoneyBonusFromTrailer(shopItem.GetMoneyBonus());
    }

    public void SetRailway(Railway railway)
    {
        _railway = railway;
    }

    public Railway GetRailway()
    {
        return _railway;
    }

    private void SetMoneyBonusFromTrailer(int moneyBonus)
    {
        _moneyBonusFromTrailer = moneyBonus;
    }

    private void CountWonMoney()
    {
        if (_moneyBonusFromTrailer == 0)
        {
            _moneyBonusFromTrailer = Shop.instance.GetCurrentShopItem().GetMoneyBonus();
        }

        _countOfWonLevelCoins = _countOfFinishedTrailers * _moneyBonusFromTrailer;
        if (_countOfWonLevelCoins == 0)
        {
            _countOfWonLevelCoins = trailers.Count;
        }

        if (_countOfWonLevelCoins < 10)
        {
            _countOfWonLevelCoins = 10;
        }

        var collectedDuringLevel = Bank.instance.GetCoins();
        Bank.instance.RemoveCoins();
        _countOfWonLevelCoins += collectedDuringLevel;

        Bank.instance.AddBalance(_countOfWonLevelCoins);
    }

    private int CalculateFinishMultiplyCoins(float countOfFinishedTrailers)
    {
        var multiplier = countOfFinishedTrailers switch
        {
            >= 13 and <= 16 => 5,
            >= 9 and <= 12 => 4,
            >= 5 and <= 8 => 3,
            >= 1 and <= 4 => 2,
            _ => 1
        };

        return multiplier;
    }

    IEnumerator VictoryGameCoroutine()
    {
        _isVictory = true;
        SetCurrentLevelIsDone();

        //запустить анимацию победы
        //UIController.instance.CloseView();
        if (_countOfFinishedTrailers > 0)
        {
            finishPerformanceEventChannel.RaiseEvent();
            victorySfx.Play();
            mainMusic.Stop();
            yield return new WaitForSecondsRealtime(1f);
            mainMusic.Play();
            yield return new WaitForSecondsRealtime(1.5f);
        }

        ClearTrailerList();

        AdManager.Instance.IncrementCountOfLevels();
        
        victoryEventChannel.RaiseEvent();
    }

    private void SetCurrentLevelIsDone()
    {
        LevelManager.instance.SetCurrentLevelIsDone();
    }

    public Trailer GetMainTrailerFromTrailers()
    {
        foreach (var trailer in trailers)
        {
            if (trailer.IsMainTrailer())
            {
                _mainTrailer = trailer;
                return trailer;
            }
        }

        return null;
    }

    public Trailer GetMainTrailer() => _mainTrailer;

    private async UniTask Defeat()
    {
        Bank.instance?.RemoveCoins();
        defeatSfx.Play();
        await UniTask.Delay(TimeSpan.FromSeconds(defeatSfx.Duration));

        defeatEventChannel.RaiseEvent();
    }
}