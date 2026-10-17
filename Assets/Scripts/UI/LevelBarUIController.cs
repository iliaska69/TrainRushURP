using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelBarUIController : MonoBehaviour
{
    [SerializeField] private Image _filledImage;
    [SerializeField] private TextMeshProUGUI _levelText;

    private float _lerpIteraction = 2f;

    private void Start()
    {
        _filledImage.fillAmount = 0;
        _levelText.text = LevelManager.instance.GetNumberOfLevel().ToString();
    }

    private void Update()
    {
        var targetValue = SliderLevelBar.instance.GetSliderValue();
        if (targetValue > 0.9f)
            _lerpIteraction = 5f;

        _filledImage.fillAmount = Mathf.Lerp(_filledImage.fillAmount, targetValue, Time.deltaTime * _lerpIteraction);
    }
}