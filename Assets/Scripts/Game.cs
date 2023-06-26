using UnityEngine;

public class Game : MonoBehaviour
{
    public void FreezeGame()
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime= 0f;
    }

    public void UnfreezeGame()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 1f;
    }
}
