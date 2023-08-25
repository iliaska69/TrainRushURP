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

    private List<Trailer> trailers = new List<Trailer>();
    private int _countOfFinishTrailers;
    private bool _isVictory;

    
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
    }

    public void SetIsVictory(bool value)
    {
        _isVictory = value;
    }

    public void IncrementFinishTrailer()
    {
        _countOfFinishTrailers++;
        if (_countOfFinishTrailers != trailers.Count) return;


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
        if(trailers.Count == 0 )
            defeatEventChannel.RaiseEvent();
    }

    public Trailer GetFirstTrailer()
    {
        return trailers[0];
    }

    public void ClearTrailerList()
    {
        trailers.Clear();
        _countOfFinishTrailers = 0;
    }

    public int GetCountOfTrailers()
        => trailers.Count;

    public List<Trailer> GetTrailerList()
        => trailers;
}
