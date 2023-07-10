using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO defeatEventChannel;
    private int countOfTrailers = 1;


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
        Debug.Log(countOfTrailers);
    }

    public void DecrementCountOfTrailers()
    {
        countOfTrailers--;
        Debug.Log(countOfTrailers);
        if (countOfTrailers == 0)
            defeatEventChannel.RaiseEvent();
    }


}
