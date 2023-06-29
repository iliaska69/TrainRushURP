using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Camera UICamera;
    private int _currentLevel;


    private void Start()
    {
        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
    }

    public void LoadLeveleScene()
    {
        SceneManager.LoadSceneAsync("FirstLevelScene", LoadSceneMode.Additive);
        _currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
        UICamera.gameObject.SetActive(false);
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

    public void UnloadLevelSecene()
    {
        UICamera.gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync(_currentLevel);
    }
}
