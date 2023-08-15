using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private Transform railway;
    [SerializeField] private TrailerEventChanelSO changedMainTrailerEventSO;
    private List<Trailer> savedTrailers = new List<Trailer>();


    public void Save()
    {
        foreach (var trailer in savedTrailers)
        {
            Destroy(trailer.gameObject);
        }
        savedTrailers.Clear();

        var trailers = Game.instance.GetTrailerList();
        if (trailers.Count == 1)
            Instantiate(trailers[trailers.Count - 1]);
        else
        {
            foreach (var trailer in trailers)
            {
                var trailerMovement = trailer.GetComponent<TrailerMovement>();
                var newTrailer = Instantiate(trailer, railway);

                var newTrailerMovement = newTrailer.GetComponent<TrailerMovement>();
                newTrailerMovement.SetStartPosition(
                    trailerMovement.GetBezierCurve(),
                    trailerMovement.GetTParametr());

                savedTrailers.Add(newTrailer);
            }

            for (var i = 0; i < savedTrailers.Count; i++)
            {
                if (i == 0)
                {
                    savedTrailers[i].SetNextTrailer(savedTrailers[i + 1]);
                }
                else if (i == savedTrailers.Count - 1)
                {
                    savedTrailers[i].SetPreviousTrailer(savedTrailers[i - 1]);
                }
                else
                {
                    savedTrailers[i].SetNextTrailer(savedTrailers[i + 1]);
                    savedTrailers[i].SetPreviousTrailer(savedTrailers[i - 1]);
                }

                savedTrailers[i].gameObject.SetActive(false);
            }

        }

    }

    public void ActivateSavedTrailers()
    {
        Game.instance.ClearTrailerList();
        for (var i = 0; i < savedTrailers.Count; i++)
        {
            savedTrailers[i].gameObject.SetActive(true);
            if (i == savedTrailers.Count - 1)
            {
                changedMainTrailerEventSO.RaiseEvent(savedTrailers[i]);
                continue;
            }
            Game.instance.IncrementCountOfTrailers(savedTrailers[i]);
        }
    }
}