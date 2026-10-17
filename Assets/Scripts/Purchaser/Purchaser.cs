using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Serialization;

public class Purchaser : MonoBehaviour, IStoreListener
{
    [SerializeField] private View welcomeView;
    private IStoreController _storeController;
    private HashSet<string> _processedPurchases = new();
    public static Purchaser Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    // Идентификаторы товаров
    private const string REMOVE_ADS = "railwayracers.removeads1";
    private const string COINS_200 = "railwayracers.200coins";
    private const string COINS_450 = "railwayracers.450coins";
    private const string COINS_750 = "railwayracers.750coins";
    private const string COINS_1400 = "railwayracers.1400coins";
    private const string COINS_2400 = "railwayracers.2400coins";
    private const string COINS_4000 = "railwayracers.4000coins";
    
    private readonly Dictionary<int, string> _coinsGoods = new()
    {
        { 200, COINS_200 },
        { 450, COINS_450 },
        { 750, COINS_750 },
        { 1400, COINS_1400 },
        { 2400, COINS_2400 },
        { 4000, COINS_4000 },
    };

    private void Start()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Регистрируем товары
        builder.AddProduct(REMOVE_ADS, ProductType.NonConsumable);
        builder.AddProduct(COINS_200, ProductType.Consumable);
        builder.AddProduct(COINS_450, ProductType.Consumable);
        builder.AddProduct(COINS_750, ProductType.Consumable);
        builder.AddProduct(COINS_1400, ProductType.Consumable);
        builder.AddProduct(COINS_2400, ProductType.Consumable);
        builder.AddProduct(COINS_4000, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
        //LoadProcessedPurchases();
        
    }

    public void BuyRemoveAds()
    {
        BuyProductID(REMOVE_ADS);
    }

    public void BuyCoins(int countOfCoins)
    {
        if (!_coinsGoods.TryGetValue(countOfCoins, out var countOfCoinsConstant))
        {
            Debug.LogError($"Не нашел такой донат: {countOfCoinsConstant}");
            return;
        }
        
        BuyProductID(countOfCoinsConstant);
    }

    public List<Product> GetCoinsPrices()
    {
        List<Product> coinProducts = new();
        foreach (var kvp in _coinsGoods)
        {
            var product = _storeController.products.WithID(kvp.Value);
            coinProducts.Add(product);
        }
        
        return coinProducts;
    }
    
    private void BuyProductID(string productId)
    {
        if (_storeController != null)
        {
            _storeController.InitiatePurchase(productId);
            
        }
        else
        {
            Debug.LogError("IAP не инициализирован!");
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        _storeController = controller;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError("Ошибка инициализации IAP: " + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message = null)
    {
        Debug.LogError("Ошибка инициализации IAP: " + error);
    }
    
    private void LoadProcessedPurchases()
    {
        var saved = PlayerPrefs.GetString("processed_purchases", "");
        if (!string.IsNullOrEmpty(saved))
            _processedPurchases = new HashSet<string>(saved.Split('|'));
    }

    private void SaveProcessedPurchases()
    {
        PlayerPrefs.SetString("processed_purchases", string.Join("|", _processedPurchases));
        PlayerPrefs.Save();
    }
    
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (_processedPurchases.Contains(args.purchasedProduct.transactionID))
        {
            Debug.LogWarning($"Покупка {args.purchasedProduct.transactionID} уже обработана");
            return PurchaseProcessingResult.Complete;
        }

        _processedPurchases.Add(args.purchasedProduct.transactionID);
        //SaveProcessedPurchases();
        
        switch (args.purchasedProduct.definition.id)
        {
            case REMOVE_ADS:
                RemoveAds();
                break;
            case COINS_200:
                AddCoins(200);
                break;
            case COINS_450:
                AddCoins(450);
                break;
            case COINS_750: 
                AddCoins(750);
                break;
            case COINS_1400:
                AddCoins(1400);
                break;
            case COINS_2400:
                AddCoins(2400);
                break;
            case COINS_4000:
                AddCoins(4000);
                break;
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.LogError($"Ошибка покупки {product.definition.id}: {failureReason}");
    }

    private void RemoveAds()
    {
        PlayerPrefs.SetInt("AdsRemoved", 1);
        PlayerPrefs.SetInt("NoAdsHidden", 1);
        PlayerPrefs.Save();

        Debug.Log("Реклама отключена!");
        UIController.instance.OpenView(welcomeView);
    }

    private void AddCoins(int amount)
    {
        Debug.Log($"Начислено {amount} монет");
        Bank.instance.AddBalance(amount);
    }
}
