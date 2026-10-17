using UnityEngine;

public class CoinsManager
{
    private int _countOfCoins;
    private int _countOfLevelCoins;

    private const string COUNT_OF_COIONS = "Count of new coins";
    private const string COUNT_OF_LEVEL_COINS = "Count of new level coins";

   
    public int GetCoins()
    {
        _countOfCoins = PlayerPrefs.GetInt(COUNT_OF_COIONS);

        return _countOfCoins;
    }

    public void AddCoins(int coins)
    {
        GetCoins();
        _countOfCoins += coins;
        PlayerPrefs.SetInt(COUNT_OF_COIONS, _countOfCoins);
    }

    private void RemoveCoins()
    {
        PlayerPrefs.SetInt(COUNT_OF_COIONS, 0);
    }
    
    // public int GetLevelCoins()
    // {
    //     _countOfLevelCoins = PlayerPrefs.GetInt(COUNT_OF_LEVEL_COINS);
    //
    //     return _countOfLevelCoins;
    // }
    //
    // public void AddLevelCoins(int coins)
    // {
    //     GetLevelCoins();
    //     _countOfLevelCoins += coins;
    //     PlayerPrefs.SetInt(COUNT_OF_LEVEL_COINS, _countOfLevelCoins);
    // }
    //
    // private void RemoveLevelCoins()
    // {
    //     PlayerPrefs.SetInt(COUNT_OF_LEVEL_COINS, 0);
    // }

    // public int GetALlCoins()
    // {
    //     var f = GetLevelCoins();
    //     var a = GetCoins();
    //     return f + a;
    // }
    //
    // public void RemoveAllCoins()
    // {
    //     RemoveCoins();
    //     RemoveLevelCoins();
    // }
}
