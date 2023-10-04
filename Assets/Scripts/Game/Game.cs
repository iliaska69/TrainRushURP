using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game instance = null;

    [SerializeField] private VoidEventChannelSO defeatEventChannel;
    [SerializeField] private VoidEventChannelSO victoryEventChannel;
    [SerializeField] private VoidEventChannelSO finishEventChannel;
    [SerializeField] private Trailer trailerSkin;

    private List<Trailer> trailers = new List<Trailer>();
    private int _countOfFinishedTrailers;
    private bool _isVictory;
    private int moneyBonusFromTrailer;


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

    private void Start()
    {
        //SetMoneyBonusFromTrailer(trailerSkin)
    }

    IEnumerator VictoryGameCoroutine()
    {
        _isVictory = true;
        yield return new WaitForSecondsRealtime(4f);
        ClearTrailerList();
        victoryEventChannel.RaiseEvent();
    }

    public void VictoryGame()
    {
        if (_isVictory) return;

        StartCoroutine(VictoryGameCoroutine());
        CountWonMoney();
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
        if (trailers.Count == 0)
            defeatEventChannel.RaiseEvent();
    }

    public Trailer GetFirstTrailer()
    {
        if (trailers.Count == 0) return null;
        return trailers[0];
    }

    public void ClearTrailerList()
    {
        trailers.Clear();
        _countOfFinishedTrailers = 0;
    }

    public int GetCountOfTrailers()
        => trailers.Count;

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

    private void SetMoneyBonusFromTrailer(int moneyBonys)
    {
        moneyBonusFromTrailer = moneyBonys;
    }

    private void CountWonMoney()
    {
        if(moneyBonusFromTrailer == 0)
        {
            moneyBonusFromTrailer = Shop.instance.GetCurrentShopItem().GetMoneyBonus();
        }
        var wonMoney = _countOfFinishedTrailers * moneyBonusFromTrailer;
        Bank.instance.AddMoney(wonMoney);
    }
}