using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class Localization : MonoBehaviour
{
    private const string LanguageKey = "SELECTED_LANGUAGE";
    
    public static Localization instance;
    
    private async void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        
        await InitializeAsync();
    }

    public int GetCurrentLanguageIndex()
    {
        return LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale);
    }

    private async UniTask InitializeAsync()
    {
        await WaitForLocalizationAsync();
        
        var savedLanguageIndex = PlayerPrefs.GetInt(LanguageKey, -1);

        if (savedLanguageIndex == -1)
        {
            var systemLanguage = Application.systemLanguage;

            var defaultIndex = 0;
            for (var i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++)
            {
                var locale = LocalizationSettings.AvailableLocales.Locales[i];

                if (systemLanguage == SystemLanguage.Russian && locale.Identifier.Code.StartsWith("ru"))
                {
                    defaultIndex = i;
                    break;
                }
                if (systemLanguage == SystemLanguage.English && locale.Identifier.Code.StartsWith("en"))
                {
                    defaultIndex = i;
                    break;
                }
            }

            savedLanguageIndex = defaultIndex;
        }

        await SetLanguage(savedLanguageIndex);
    }

    public async UniTask SwitchLanguage()
    {
        var currentLanguageIndex = GetCurrentLanguageIndex();
        var newLanguageIndex = (currentLanguageIndex == 0) ? 1 : 0;
        await SetLanguage(newLanguageIndex);
    }

    private async UniTask SetLanguage(int languageIndex)
    {
        if (languageIndex < 0 || languageIndex >= LocalizationSettings.AvailableLocales.Locales.Count)
            return;

        await LocalizationSettings.InitializationOperation;

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];

        PlayerPrefs.SetInt(LanguageKey, languageIndex);
        PlayerPrefs.Save();
    }

    public async UniTask WaitForLocalizationAsync()
    {
        await LocalizationSettings.InitializationOperation.ToUniTask();
    }
}
