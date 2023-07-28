using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game instance = null;

    [SerializeField] private VoidEventChannelSO defeatEventChannel;

    private int experiencePoints; 
    private List<Trailer> trailers = new List<Trailer>();


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

    public void ClearTrailerList()
    {
        trailers.Clear();
    }

    public void DecrementCountOfTrailers(Trailer trailer)
    {
        trailers.Remove(trailer);
        if(trailers.Count == 0 )
            defeatEventChannel.RaiseEvent();
    }

    public void IncrementExperiencePoints(Trailer trailer)
    {
        
    }

    public void DecrementExperiencePoints(Trailer trailer)
    {
        
    }

    public Trailer GetFirstTrailer()
    {
        if (trailers is null) return null;

        return trailers[0];
    }
}
