using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
    private const string GAME_IS_DONE = "Game is done";
    private const string LAST_INDEXES = "List of last level indexes";
    private int _currentLevel;
    private int _lastLoadLevel;
    private int _sceneCount;
    private bool _gameIsDone;
    private List<int> _lastLevels = new List<int>();

    private void Start()
    {
        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        _sceneCount = SceneManager.sceneCountInBuildSettings;
        _gameIsDone = Convert.ToBoolean(PlayerPrefs.GetInt(GAME_IS_DONE));
        LoadLevel();
        //PlayerPrefs.DeleteKey(LAST_INDEXES);
        //PlayerPrefs.DeleteKey(GAME_IS_DONE);
    }

    public void LoadLevel()
    {
        _lastLevels = GetLastLevels();
        if(_lastLevels.Count > 0)
            _lastLoadLevel = _lastLevels[_lastLevels.Count - 1];

        //if (_sceneCount == (_currentLevel + 1) || _gameIsDone)
        //{
        //    if(_sceneCount >= 3)
        //        LoadRandomLevel();
        //    else
        //        SceneManager.LoadSceneAsync("LevelScene_1", LoadSceneMode.Additive);
        //}
        if (_lastLoadLevel <= 1)
        {
            SceneManager.LoadSceneAsync("LevelScene_1", LoadSceneMode.Additive);
            _currentLevel = SceneManager.GetActiveScene().buildIndex + 2;
            _lastLoadLevel = _currentLevel;
            Save(_lastLoadLevel);
        }
        else
        {
            _currentLevel = _lastLoadLevel;
            SceneManager.LoadSceneAsync(_currentLevel, LoadSceneMode.Additive);
        }

    }

    private void Save(int level)
    {
        _lastLevels.Add(level);
        if (_lastLevels.Count > 3)
            LeaveLastThreeElements();
        var combinedStringOfIndexes = string.Join(",", _lastLevels.ToArray());
        PlayerPrefs.SetString(LAST_INDEXES, combinedStringOfIndexes);
    }

    public void ReloadLevel()
    { 
        SceneManager.UnloadSceneAsync(_currentLevel);
        SceneManager.LoadScene(_currentLevel, LoadSceneMode.Additive);
    }

    public void LoadNextLevel()
    {
        SceneManager.UnloadSceneAsync(_currentLevel);
        if (_sceneCount == (_currentLevel + 1) || _gameIsDone)
        {
            if (_sceneCount >= 3)
                LoadRandomLevel();
            else
                SceneManager.LoadSceneAsync("LevelScene_1", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadSceneAsync(_currentLevel + 1, LoadSceneMode.Additive);
            _currentLevel++;
            _lastLoadLevel = _currentLevel;
            Save(_lastLoadLevel);
        }
    }

    public void UnloadLevel()
    {
        SceneManager.UnloadSceneAsync(_currentLevel);
    }

    private List<int> GetLastLevels()
    {
        var listOfIndexes = PlayerPrefs.GetString(LAST_INDEXES);
        if (listOfIndexes == "")
            return new List<int>();

        var lastLevelIndexes = listOfIndexes.Split(',').ToList();

        return lastLevelIndexes.Select(int.Parse).ToList();
    }

    private void LeaveLastThreeElements()
    {
        var listCount = _lastLevels.Count;
        for (var i = 0; i < listCount; i++)
        {
            if (i < listCount - 3)
            { 
                _lastLevels.RemoveAt(i);
            }
        }
        
    }

    private void LoadRandomLevel()
    {
        _gameIsDone = true;
        PlayerPrefs.SetInt(GAME_IS_DONE, Convert.ToInt32(_gameIsDone));
        var randomLevelScene = UnityEngine.Random.Range(2, _sceneCount);

        for (var i = 0; i < _lastLevels.Count; i++)
        {
            if (randomLevelScene == _lastLevels[i])
            {
                randomLevelScene = UnityEngine.Random.Range(2, (_sceneCount));
                i = -1;
            }
        }
        SceneManager.LoadSceneAsync(randomLevelScene, LoadSceneMode.Additive);
        Save(randomLevelScene);

        _currentLevel = randomLevelScene;
    }
}
