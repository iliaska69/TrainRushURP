using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int _currentLevel;


    public void LoadLeveleScene()
    {
        SceneManager.LoadSceneAsync("FirstLevelScene", LoadSceneMode.Additive);
        _currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void ReloadLevelScene()
    {
        SceneManager.UnloadSceneAsync(_currentLevel);
        SceneManager.LoadScene(_currentLevel, LoadSceneMode.Additive);
    }

    public void LoadNextLevelScene()
    {
        SceneManager.UnloadSceneAsync(_currentLevel);
        SceneManager.LoadScene(_currentLevel + 1, LoadSceneMode.Additive);
    }
}
