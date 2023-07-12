using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO defeatEventChannel;

    private int countOfTrailers = 1;
    private int experiencePoints; 
    private List<Trailer> trailers;


    public void FreezeGame()
    {
        Time.timeScale = 0f;
    }

    public void UnfreezeGame()
    {
        Time.timeScale = 1f;
    }

    public void IncrementCountOfTrailers()
    {
        countOfTrailers++;
    }

    public void DecrementCountOfTrailers()
    {
        countOfTrailers--;
        if (countOfTrailers == 0)
        {
            defeatEventChannel.RaiseEvent();
            countOfTrailers = 1;
        }
    }

    public void IncrementExperiencePoints(Trailer trailer)
    {
        var newExperience = trailer.GetPointsExperience();
        experiencePoints += newExperience;
        trailers.Add(trailer);
    }

    public void DecrementExperiencePoints(Trailer trailer)
    {
        if (experiencePoints <= 0) return;
        experiencePoints -= trailer.GetPointsExperience();
        trailers.Remove(trailer);
    }
}
