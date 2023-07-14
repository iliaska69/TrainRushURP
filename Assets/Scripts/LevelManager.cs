using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //[SerializeField] private Camera UICamera;

    private const string LAST_LEVEL = "Last level";
    private const string GAME_IS_DONE = "Game is done";
    private const string LAST_INDEXES = "List of last level indexes";
    private int _currentLevel;
    private int _lastLevel;
    private int _sceneCount;
    private int _gameIsDone;
    private List<int> _lastLevelIndexes = new List<int>();

    private void Start()
    {
        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        _sceneCount = SceneManager.sceneCountInBuildSettings;
        _gameIsDone = PlayerPrefs.GetInt(GAME_IS_DONE);
        GetLastLevelIndexes();
        LoadLevelScene();
        //PlayerPrefs.DeleteKey(LAST_LEVEL);
        //PlayerPrefs.DeleteKey(LAST_INDEXES);
        //PlayerPrefs.DeleteKey(GAME_IS_DONE);
    }

    public void LoadLevelScene()
    {
        _lastLevel = PlayerPrefs.GetInt(LAST_LEVEL);

        if (_sceneCount == (_currentLevel + 1) || _gameIsDone == 1)
        {
            LoadRandomLevelScene();
        }
        else if (_lastLevel <= 1)
        {
            SceneManager.LoadSceneAsync("LevelScene_1", LoadSceneMode.Additive);
            _currentLevel = SceneManager.GetActiveScene().buildIndex + 2;
            _lastLevel = _currentLevel;
        }
        else
        {
            _currentLevel = _lastLevel;
            SceneManager.LoadSceneAsync(_lastLevel, LoadSceneMode.Additive);
        }

        PlayerPrefs.SetInt(LAST_LEVEL, _lastLevel);
    }

    public void ReloadLevelScene() { 
        SceneManager.UnloadSceneAsync(_currentLevel);
        SceneManager.LoadScene(_currentLevel, LoadSceneMode.Additive);
    }

    public void LoadNextLevelScene()
    {
        SceneManager.UnloadSceneAsync(_currentLevel);
        if (_sceneCount == (_currentLevel + 1) || _gameIsDone == 1)
        {
            LoadRandomLevelScene();
        }
        else
        {
            SceneManager.LoadSceneAsync(_currentLevel + 1, LoadSceneMode.Additive);
            _currentLevel++;
            _lastLevel = _currentLevel;
            PlayerPrefs.SetInt(LAST_LEVEL, _lastLevel);
        }
    }

    public void UnloadLevelSecene()
    {
        SceneManager.UnloadSceneAsync(_currentLevel);
    }

    private void GetLastLevelIndexes()
    {
        var listOfIndexes = PlayerPrefs.GetString(LAST_INDEXES);
        if (listOfIndexes == "")
            return;
        var lastLevelIndexes = listOfIndexes.Split(',').ToList();
        _lastLevelIndexes = lastLevelIndexes.Select(int.Parse).ToList();
    }

    private void LoadRandomLevelScene()
    {
        _gameIsDone = 1;
        PlayerPrefs.SetInt(GAME_IS_DONE, _gameIsDone);
        var randomLevelScene = Random.Range(2, _sceneCount);
        for (var i = 0; i < _lastLevelIndexes.Count; i++)
        {
            if (randomLevelScene == _lastLevelIndexes[i])
            {
                randomLevelScene = Random.Range(2, (_sceneCount-3));
                i = 0;
            }
        }
        if (_lastLevelIndexes.Count == 3)
        {
            _lastLevelIndexes.RemoveAt(0);
        }

        _lastLevelIndexes.Add(randomLevelScene);
        var combinedStringOfIndexes = string.Join(",", _lastLevelIndexes.ToArray());
        PlayerPrefs.SetString(LAST_INDEXES, combinedStringOfIndexes);
        Debug.Log(PlayerPrefs.GetString(LAST_INDEXES));


        SceneManager.LoadSceneAsync(randomLevelScene, LoadSceneMode.Additive);

        _currentLevel = randomLevelScene;
    }
}
