using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;

    [SerializeField] private UnityEvent onLevelLoaded;

    private const string GAME_IS_DONE = "Game is done";
    private const string LAST_INDEXES = "List of last level indexes";
    private const string FIRST_LEVEL = "LevelScene_1";
    private const string SHOP_SCENE = "ShopScene";
    private const string NUMBER_OF_LEVEL = "Number of level";
    private int _currentLevel;
    private int _lastLoadLevel;
    private int _sceneCount;
    private bool _gameIsDone;
    private List<int> _lastLevels = new();
    private int _numberOfLevel=1;

    public bool SceneLevelIsLoaded { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // PlayerPrefs.DeleteKey(LAST_INDEXES);
        // PlayerPrefs.DeleteKey(GAME_IS_DONE);
        // PlayerPrefs.DeleteKey(NUMBER_OF_LEVEL);
        
        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        _sceneCount = SceneManager.sceneCountInBuildSettings;
        _gameIsDone = Convert.ToBoolean(PlayerPrefs.GetInt(GAME_IS_DONE));
        var numberOfLevel = PlayerPrefs.GetInt(NUMBER_OF_LEVEL);
        if (numberOfLevel != 0)
            _numberOfLevel = numberOfLevel;
        
        LoadLevel();
    }

    public void LoadLevel()
    {
        _lastLevels = GetLastLevels();
        if (_lastLevels.Count > 0)
            _lastLoadLevel = _lastLevels[_lastLevels.Count - 1];

        if (_lastLoadLevel <= 1)
        {
            StartCoroutine(LoadSceneAsync(FIRST_LEVEL, LoadSceneMode.Additive));
            
            _currentLevel = SceneManager.GetActiveScene().buildIndex + 3;
            _lastLoadLevel = _currentLevel;
            Save(_lastLoadLevel);
        }
        else
        {
            _currentLevel = _lastLoadLevel;
            StartCoroutine(LoadSceneAsync(_currentLevel, LoadSceneMode.Additive));
        }
    }

    public int GetNumberOfLevel() => _numberOfLevel;

    public void IncrementNumberOnLevel()
    {
        PlayerPrefs.SetInt(NUMBER_OF_LEVEL, ++_numberOfLevel);
    }
    
    public void LoadLevelAdmin(string level)
    {
        var newCurrentLevel = int.Parse(level);
        SceneManager.UnloadSceneAsync("GameScene");
        SceneManager.UnloadSceneAsync(_currentLevel);
        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        StartCoroutine(LoadSceneAsync(newCurrentLevel + 2, LoadSceneMode.Additive));
        _currentLevel = newCurrentLevel + 2;
        Debug.Log(_currentLevel);
    }
    
    IEnumerator LoadSceneAsync(int currentLevel, LoadSceneMode mode)
    {
        SceneLevelIsLoaded = false;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentLevel, mode);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneLevelIsLoaded = true;
        var scene = SceneManager.GetSceneByBuildIndex(_currentLevel);
        SceneManager.SetActiveScene(scene);

        ResetLevelCollectedCoins();
        Game.instance.SetIsVictory(false);
        //AdManager.Instance.IncrementCountOfLevels();
    }

    IEnumerator UnloadShopSceneAndLoadLevel(int currentLevel, LoadSceneMode mode)
    {
        SceneLevelIsLoaded = false;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentLevel, mode);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneLevelIsLoaded = true;
        var scene = SceneManager.GetSceneByBuildIndex(_currentLevel);
        SceneManager.SetActiveScene(scene);
        SceneManager.UnloadSceneAsync(SHOP_SCENE);

        ResetLevelCollectedCoins();
        Game.instance.SetIsVictory(false);
    }
    
    IEnumerator LoadSceneAsync(string currentLevel, LoadSceneMode mode)
    {
        SceneLevelIsLoaded = false;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentLevel, mode);
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(FIRST_LEVEL));
        SceneLevelIsLoaded = true;

        ResetLevelCollectedCoins();
        Game.instance.SetIsVictory(false);
        //AdManager.Instance.IncrementCountOfLevels();
    }

    private void Save(int level)
    {
        _lastLevels.Add(level);
        if (_lastLevels.Count > 3)
            LeaveLastThreeElements();
        var combinedStringOfIndexes = string.Join(",", _lastLevels.ToArray());
        PlayerPrefs.SetString(LAST_INDEXES, combinedStringOfIndexes);
        PlayerPrefs.Save();
    }

    public void ReloadLevel()
    {
        SceneManager.UnloadSceneAsync(_currentLevel);
        StartCoroutine(LoadSceneAsync(_currentLevel, LoadSceneMode.Additive));
        Game.instance.SetIsVictory(false);
    }

    public void LoadNextLevel()
    {
        if (_gameIsDone)
        {
            SceneManager.UnloadSceneAsync(_currentLevel);
            LoadRandomLevel();
        }
        else
        {
            SceneManager.UnloadSceneAsync(_lastLevels[^2]);
            _lastLevels = GetLastLevels();
            _currentLevel = _lastLevels[^1];
        
            StartCoroutine(LoadSceneAsync(_currentLevel, LoadSceneMode.Additive));
        }
        
        
        Game.instance.SetIsVictory(false);
    }

    private List<int> GetLastLevels()
    {
        var listOfIndexes = PlayerPrefs.GetString(LAST_INDEXES);
        if (listOfIndexes == "")
            return new List<int>();

        var lastLevelIndexes = listOfIndexes.Split(',').ToList();

        return lastLevelIndexes.Select(int.Parse).ToList();
    }

    public void OpenShopScene()
    {
        Game.instance.ClearTrailerList();
        StartCoroutine(LoadShopSceneAsync());
    }

    IEnumerator LoadShopSceneAsync()
    {
        SceneLevelIsLoaded = false;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SHOP_SCENE, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync(_currentLevel);
        SceneLevelIsLoaded = true;

        Game.instance.SetIsVictory(false);
    }


    public void UnloadShopScene()
    {
        StartCoroutine(UnloadShopSceneAndLoadLevel(_currentLevel, LoadSceneMode.Additive));
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
        _lastLevels = GetLastLevels();
        var randomLevelScene = UnityEngine.Random.Range(5, _sceneCount);
        for (var i = 0; i < _lastLevels.Count; i++)
        {
            if (randomLevelScene == _lastLevels[i])
            {
                randomLevelScene = UnityEngine.Random.Range(5, _sceneCount);
                i = -1;
            }
        }
        
        _currentLevel = randomLevelScene;
        StartCoroutine(LoadSceneAsync(_currentLevel, LoadSceneMode.Additive));
        Save(_currentLevel);
    }

    public void SetCurrentLevelIsDone()
    {
        var level = _currentLevel;
        level++;
        if(_sceneCount == level)
        {
            _gameIsDone = true;
            PlayerPrefs.SetInt(GAME_IS_DONE, Convert.ToInt32(_gameIsDone));
        }
        else
        {
            if (_gameIsDone)
                return;

            _currentLevel++;
            Save(_currentLevel);
        }
    }

    private static void ResetLevelCollectedCoins()
    {
        if (Bank.instance != null)
            Bank.instance.RemoveCoins();
    }
}