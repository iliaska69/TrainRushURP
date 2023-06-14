using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int _countOfTreilers;

    public void IncrementCountOfTrailers()
    {
        _countOfTreilers++;
    }

    public void DecrementCountOfTrailers()
    {
        _countOfTreilers--;
        if (_countOfTreilers == 0)
        {
            ReloadScene();
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadSceneAsync("FirstScene");
    }
}
