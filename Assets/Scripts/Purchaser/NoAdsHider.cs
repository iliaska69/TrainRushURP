using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class NoAdsHider : MonoBehaviour
{
    [SerializeField] private Button _noAdsButton;
    private const string NoAdsKey = "NoAdsHidden";

    private void Start()
    {
        if (PlayerPrefs.GetInt(NoAdsKey, 0) == 1)
        {
            _noAdsButton.gameObject.SetActive(false);
        }
    }

    public void HideButton()
    {
        _noAdsButton.gameObject.SetActive(false);
    }
}
