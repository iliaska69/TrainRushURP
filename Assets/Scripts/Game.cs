using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private float timerForReload;

    private int _countOfTreilers = 1;
    private bool isStartTimerForReload;


    private void Update()
    {
        if (isStartTimerForReload)
        {
            timerForReload -= Time.deltaTime;
        }
        if (timerForReload <= 0)
        {
            levelManager.LoadScene();
        }
    }

    public void IncrementCountOfTrailers()
    {
        _countOfTreilers++;
    }

    public void DecrementCountOfTrailers()
    {
        _countOfTreilers--;
        
        if (_countOfTreilers == 0)
        {
            isStartTimerForReload= true;
        }
    }
}
