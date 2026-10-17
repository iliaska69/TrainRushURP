using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Daily Reward", menuName = "ScriptableObjects/Daily Reward", order = 3)]
public class DailyRewardSO : ScriptableObject
{
    public DailyRewardUI dailyRewardUI; 
    public int coins = 0;
    public int tickets = 0;
    public int cars = 0;
}
