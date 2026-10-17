using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CollectingCoin : MonoBehaviour
{
    [SerializeField] private GameObject collectingCoinPrefab;
    [SerializeField] private RectTransform spawnLocation;
    [SerializeField] private RectTransform endPosition;
    [SerializeField] private Transform coinParent;
    [SerializeField] private int coinAmountInAnimation;

    [Header("Coins Text")] [SerializeField]
    private TextMeshProUGUI countOfCoinsText;

    [SerializeField] private RectTransform bonusCountOfCoinsRectTransform;
    [SerializeField] private CanvasGroup bonusCountOfCoinsCanvasGroup;
    [SerializeField] private TextMeshProUGUI bonusCountOfCoinsText;
    [SerializeField] private TextMeshProUGUI bonusCountOfCoinsTextShadow;

    [Header("Coins Spawn")] [SerializeField]
    private float minX;

    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private float duration;

    [Header("Coins Spawn")] 
    [SerializeField] private CoinEffect coinEffect;
    
    [Header("Effects")] 
    [SerializeField] private CircleCoinEffect circleEffect;
    
    [Header("Collect Sound")] 
    [SerializeField] private SfxSound collectCoinSfx;
    [SerializeField] private SfxSound entryCoinSfx;

    [Header("Block image")] 
    [SerializeField] private GameObject blockImage;
    
    private List<GameObject> _coins = new();
    private Tween _coinPunchTween;
    private int _countOfBankCoins;
    private int _coinsToActivate;
    private int _coinsIncrement;

    private float moveCoinArcY = 0.2f;
    private float moveCoinArcX = 0.5f;

    private UIController _uiController;
    private Vector3 _bonusTextStartLocalPosition;
    private bool _isTouchedCollider;


    private async void Start()
    {
        blockImage.SetActive(true);
        _coinsToActivate = Bank.instance.GetCoins();
        
        BonusCoinsTextInstantiate();

        if (_coinsToActivate <= 0)
        {
            blockImage.SetActive(false);
            return;
        }

        
        //_uiController.DisableUIInteraction();

        await UniTask.Delay(TimeSpan.FromSeconds(0.3f));

        CollectingCoins();
        Bank.instance.RemoveCoins();
    }

    private void BonusCoinsTextInstantiate()
    {
        bonusCountOfCoinsCanvasGroup.alpha = 0;
    }

    private async void CollectingCoins()
    {
        if (_coinsToActivate <= 0) return;

        // _uiController.DisableUIInteraction();
        blockImage.SetActive(true);
        
        _countOfBankCoins = Bank.instance.GetBalance();
        _countOfBankCoins -= _coinsToActivate;

        _coinsIncrement = _coinsToActivate / coinAmountInAnimation;
        
        var position = spawnLocation.localPosition;
        CountOfCoinsTextAnim();
        for (var i = 0; i < coinAmountInAnimation; i++)
        {
            var coinInstance = Instantiate(collectingCoinPrefab, coinParent);
            var coinTransform = coinInstance.transform;
            var xPosition = position.x + Random.Range(minX, maxX);
            var yPosition = position.y + Random.Range(minY, maxY);
            coinTransform.localPosition = new Vector3(xPosition, yPosition, 0);
            coinTransform.localScale = Vector3.zero;

            var sequence = DOTween.Sequence();
            sequence.Append(coinTransform.DOScale(1.3f, 0.2f).SetEase(Ease.OutQuad));
            sequence.Append(coinTransform.DOScale(1.0f, 0.1f).SetEase(Ease.InQuad));
            sequence.Append(coinTransform.DOScale(0.7f, duration).SetEase(Ease.Linear));
            sequence.Play();

            coinTransform.DORotate(new Vector3(0, 0, 360), 3f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental);

            entryCoinSfx.Play();
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.02f));


            _coins.Add(coinInstance);
        }


        await MovesCoinsTask();

        await FinishCollectingCoins();
        blockImage.SetActive(false);
    }

    private void CountOfCoinsTextAnim()
    {
        _bonusTextStartLocalPosition = bonusCountOfCoinsRectTransform.localPosition;
        bonusCountOfCoinsText.text = "+" + _coinsToActivate;
        bonusCountOfCoinsTextShadow.text = "+" + _coinsToActivate.ToString();

        var sequence = DOTween.Sequence();
        sequence
            .Append(bonusCountOfCoinsCanvasGroup.DOFade(1, 0.5f).SetEase(Ease.Linear))
            .Join(bonusCountOfCoinsRectTransform
                .DOLocalMove(_bonusTextStartLocalPosition + new Vector3(0, 100, 0), 0.8f).SetEase(Ease.Linear))
            .Append(bonusCountOfCoinsRectTransform
                .DOLocalMove(_bonusTextStartLocalPosition + new Vector3(0, 200, 0), 0.8f).SetEase(Ease.Linear))
            .Append(bonusCountOfCoinsRectTransform.DOLocalMove(_bonusTextStartLocalPosition + new Vector3(0, 300, 0),
                0.8f)).SetEase(Ease.Linear)
            .Join(bonusCountOfCoinsCanvasGroup.DOFade(0, 0.5f)).SetEase(Ease.Linear);
    }

    private async UniTask FinishCollectingCoins()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));

        _coinsToActivate = 0;
    }

    private async UniTask MovesCoinsTask()
    {
        var moveCoinTask = new List<UniTask>();

        foreach (var coin in _coins)
        {
            moveCoinTask.Add(MoveCoinTask(coin));
            await UniTask.Delay(TimeSpan.FromSeconds(0.02f));
        }

        await UniTask.WhenAll(moveCoinTask);


        ClearListCoins();
    }

    private async UniTask MoveCoinTask(GameObject coin)
    {
        var startPoint = coin.transform.position;
        var endPoint = endPosition.position;

        var controlPoint = (startPoint + endPoint) / 2;
        controlPoint.y += moveCoinArcY;
        controlPoint.x += moveCoinArcX;
        Vector3[] path = { startPoint, controlPoint, endPoint };

        await coin.transform.DOPath(path, duration, PathType.CatmullRom).SetEase(Ease.InOutQuad).AsyncWaitForCompletion();

        SetCoin(_coinsIncrement);
    }

    private async UniTask PunchEndCoin()
    {
        if (_coinPunchTween != null) return;

        _coinPunchTween = endPosition.DOPunchScale(new Vector3(.3f, .3f), 0.1f).SetEase(Ease.InOutElastic);
        await _coinPunchTween.AsyncWaitForCompletion();
        _coinPunchTween = null;
    }

    private void ClearListCoins()
    {
        foreach (var coin in _coins)
        {
            Destroy(coin);
        }

        _coins.Clear();
    }

    private void SetCoin(int value)
    {
        _countOfBankCoins += value;
        if (_countOfBankCoins >= 1000)
        {
            var newIntCoins = (float)_countOfBankCoins / 1000;
            var newFloatCoins = (float)Math.Round(newIntCoins, 2);
            countOfCoinsText.text = newFloatCoins + "k";
        }
        else
        {
            countOfCoinsText.text = _countOfBankCoins.ToString();
        }
    }

    public void CollideWithUICoin()
    {
        PunchEndCoin();
        circleEffect.Play();
        coinEffect.PlayPunchStarsEffect();
        PlayCollectCoinSfx();
    }
    
    private void PlayCollectCoinSfx() => collectCoinSfx.Play();
}