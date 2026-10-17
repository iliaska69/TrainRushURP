using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UINewRandomTrailer : MonoBehaviour
{
    [SerializeField] private Image hiddenTrailerImage;
    [SerializeField] private Image sliderImage;
    [SerializeField] private Image newRandommTrailerImage;
    [SerializeField] private float fillDuration;
    [SerializeField] private GameObject newRandomTrailerSlice;
    [SerializeField] private TextMeshProUGUI percentageText;
    [SerializeField] private float fullDuration;
    [SerializeField] private SfxSound percentSfx;
    [SerializeField] private Button getButton;
    [SerializeField] private Button noThanksButton;
    [SerializeField] private bool applyTrailerOnTake;

    private CanvasGroup _canvasGroup;
    private ShopItem _randomShopItem;
    private static ShopItem _pendingTrailerShopItem;
    private bool _isOpen;
    private float _onePercentDuration;
    private int _previousPercentage = -1;
    
    void Start()
    {
        _canvasGroup = newRandomTrailerSlice.GetComponent<CanvasGroup>();
        newRandomTrailerSlice.SetActive(false);
        
        StartCoroutine(FillSlider());
    }

    private void Update()
    {
        if (_isOpen)
        {
            OpenNewTrailerSlice();
        }
    }

    private IEnumerator FillSlider()
    {
        sliderImage.fillAmount = NewRandomTrailerController.instance.GetPreviousPercents();

        var endValue = NewRandomTrailerController.instance.GetPercents();
        if (endValue >= 1f)
        {
            endValue = 1f;
            getButton.enabled = false;
            noThanksButton.enabled = false;
        }
        
        var startPercentage = Mathf.RoundToInt(sliderImage.fillAmount * 100);
        var targetPercentage = Mathf.RoundToInt(endValue * 100);

        for (var i = startPercentage; i <= targetPercentage; i++)
        {
            var fillAmount = i / 100f;
            sliderImage.fillAmount = fillAmount;

            percentageText.text = $"{i}%";

            if (i != _previousPercentage)
            {
                percentSfx.Play();
                _previousPercentage = i;
            }

            yield return new WaitForSeconds(0.05f);
        }

        // Завершение анимации
        var finalPercentage = Mathf.RoundToInt(endValue * 100);
        percentageText.text = $"{finalPercentage}%";
        if (sliderImage.fillAmount >= 1f)
        {
            getButton.enabled = true;
            noThanksButton.enabled = true;
            _isOpen = true;
        }
    }
    
    private void OpenNewTrailerSlice()
    {
        _isOpen = false;
        _randomShopItem = NewRandomTrailerController.instance.TakeNewRandomTrailer();
        if (_randomShopItem == null)
        {
            sliderImage.fillAmount = 0;
            NewRandomTrailerController.instance.SetPercents(0f);
            getButton.enabled = true;
            noThanksButton.enabled = true;
            
            return;
        }
        newRandommTrailerImage.sprite = _randomShopItem.trailerUiImage;
        newRandomTrailerSlice.SetActive(true);
        _canvasGroup.interactable = true;
        _canvasGroup.DOFade(1, 0.5f);
    }
    
    private void CloseNewTrailerSlice()
    {
        _canvasGroup.DOFade(0, 0.5f);
        newRandomTrailerSlice.SetActive(false);
        _canvasGroup.interactable = false;
        getButton.enabled = true;
        noThanksButton.enabled = true;
    }
    
    public void TakeItClick()
    {
        if (_randomShopItem == null) return;

        if (applyTrailerOnTake)
        {
            ApplyTrailer(_randomShopItem);
            _pendingTrailerShopItem = null;
        }
        else
        {
            _pendingTrailerShopItem = _randomShopItem;
        }

        ResetValues();
        CloseNewTrailerSlice();
    }

    public void LoseItClick()
    {
        _pendingTrailerShopItem = null;
        ResetValues();
        CloseNewTrailerSlice();
    }

    public static void ApplyPendingTrailerIfAny()
    {
        if (_pendingTrailerShopItem == null) return;

        ApplyTrailer(_pendingTrailerShopItem);
        _pendingTrailerShopItem = null;
    }

    private static void ApplyTrailer(ShopItem shopItem)
    {
        shopItem.isBuy = true;
        Shop.instance.SetShopItemIsBuy(shopItem);
        Shop.instance.SetCurrentShopItem(shopItem);
        Game.instance.SetTrailerSkin(shopItem);
    }

    private void ResetValues()
    {
        sliderImage.fillAmount = 0;
        percentageText.text = "0%";
        NewRandomTrailerController.instance.SetPercents(0f);
    }
}