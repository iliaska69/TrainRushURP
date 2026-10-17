using UnityEngine;
using UnityEngine.UI;

public class LanguageSwitcherUI : MonoBehaviour
{
    [SerializeField] private Button languageButton;
    [SerializeField] private Sprite ruSprite;
    [SerializeField] private Sprite enSprite;
    [SerializeField] private ClickButtonAnimation clickButtonAnimation;

    private async void OnEnable()
    {
        languageButton.interactable = false;

        await Localization.instance.WaitForLocalizationAsync();

        languageButton.interactable = true;
        UpdateLanguageImage();
    }

    public async void SwitchLanguage()
    {
        languageButton.interactable = false;

        await Localization.instance.SwitchLanguage();

        languageButton.interactable = true;
        UpdateLanguageImage();
        clickButtonAnimation.OnButtonClick();
    }

    private void UpdateLanguageImage()
    {
        var index = Localization.instance.GetCurrentLanguageIndex();
        languageButton.image.sprite = 
            (index == 0) 
                ? ruSprite 
                : enSprite;
    }
}
