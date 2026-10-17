using NaughtyAttributes;
using UnityEngine;

public class ResetSaves : MonoBehaviour
{
    [SerializeField] private int maxShopItems = PlayerPrefsKeys.MaxShopItems;

    [Button("Reset Saves")]
    private void ResetSavesButton()
    {
        PlayerPrefsKeys.DeleteAll(maxShopItems);
    }
}
