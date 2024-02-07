using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Daily Reward", menuName = "ScriptableObjects/Daily Reward", order = 3)]
public class DailyRewardSO : ScriptableObject
{
    public Sprite rewardSprite; 
    public int coins = 0;
    public int cristals = 0;
}
