using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    [SerializeField] private float animationDuration;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI multiplyText;
    [SerializeField] private TextMeshProUGUI _countOfWonCoins;
    [SerializeField] private UINavigator openWatchAdView;

    private static int _cachedLevelCoins;
    private static int _cachedMultiply;

    private float timer;
    private bool isForward = true;
    private int _newCurrentMultiply;
    private int _oldCurrentMultiply;
    private int _levelCoins;


    private void Start()
    {
        _levelCoins = Bank.instance.GetCoins();
        _countOfWonCoins.text = "+" + _levelCoins;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        var progress = timer / animationDuration;
        var easedProgress = Mathf.Sin(progress * Mathf.PI * 0.5f);
        var currentValue = isForward ? easedProgress : 1f - easedProgress;
        slider.value = currentValue;

        DisplayButtonCoinsBySliderValue(currentValue);

        if (timer >= animationDuration)
        {
            timer = 0f;
            isForward = !isForward;
        }
    }

    private void DisplayButtonCoinsBySliderValue(float value)
    {
        switch (value)
        {
            //2x
            case > 0 and < .125f:
                _newCurrentMultiply = 2;
                break;
            //3x
            case > .126f and < .272f:
                _newCurrentMultiply = 3;
                break;
            //4x
            case > .273f and < .415f:
                _newCurrentMultiply = 4;
                break;
            //5x
            case > .416f and < .568f:
                _newCurrentMultiply = 5;
                break;
            //4x
            case > .569f and < .717f:
                _newCurrentMultiply = 4;
                break;
            //3x
            case > .718f and < .865f:
                _newCurrentMultiply = 3;
                break;

            //2x
            case > .869f and < 1:
                _newCurrentMultiply = 2;
                break;
        }

        if (_oldCurrentMultiply == _newCurrentMultiply)
            return;

        _oldCurrentMultiply = _newCurrentMultiply;
        multiplyText.text = _newCurrentMultiply.ToString();
    }

    public void OpenWatchAdView()
    {
        _cachedLevelCoins = _levelCoins;
        _cachedMultiply = _newCurrentMultiply;
        openWatchAdView?.OpenView();
    }

    public void MultiplyCoins()
    {
        ApplyMultiply(_levelCoins, _newCurrentMultiply);
    }

    public static void ApplyCachedMultiplyCoins()
    {
        ApplyMultiply(_cachedLevelCoins, _cachedMultiply);
        _cachedLevelCoins = 0;
    }

    private static void ApplyMultiply(int levelCoins, int multiply)
    {
        var multiplyCoins = levelCoins * multiply;
        Bank.instance.SubstractBalance(levelCoins);
        Bank.instance.RemoveCoins();
        Bank.instance.AddBalance(multiplyCoins);
    }

    private void SendCoinsToBank(int coins)
    {
        //_coinsManager.AddCoins(coins);
        //Bank.instance.AddMoney(coins);
    }

    public void NoThanksButtonClick()
    {
        Debug.Log("NO THANKS CLICK");
        //SendCoinsToBank(_levelCoins);
    }
}