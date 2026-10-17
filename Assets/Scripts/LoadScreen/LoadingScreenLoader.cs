using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LoadingScreenLoader : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO openWelcomeMenuEventSo;
    
    [Header("Screen")]
    [SerializeField] private float delayLoading = 2f;
    [SerializeField] private float fadeScreenLoaderDuration = 1;
    [SerializeField] private Image loadingScreenImage;
    [SerializeField] private Image loadingSliderImage;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private Image blackScreen;

    private Tween _rotationTween;

    private async void Start()
    {
        loadingSliderImage.fillAmount = 0f;
        StartCoroutine(LoadingStartView());
        await NetworkTimeKeeper.InitializeAsync();
    }

    IEnumerator LoadingStartView()
    {
        _rotationTween = loadingSliderImage.DOFillAmount(1f, delayLoading);
        
        var displayedValue = 0;
        Tween valueTween = DOTween.To(() => displayedValue, x => {
            displayedValue = x;
            progressText.text = displayedValue.ToString();
        }, 100, delayLoading);
        
        yield return new WaitForSecondsRealtime(delayLoading);

        while (!LevelManager.instance.SceneLevelIsLoaded)
        {
            yield return null;
        }
        
        yield return valueTween.WaitForCompletion();
        
        var fadeTween = loadingScreenImage.DOFade(0, fadeScreenLoaderDuration);
        canvasGroup.DOFade(0, fadeScreenLoaderDuration);
        blackScreen.DOFade(0, fadeScreenLoaderDuration);

        yield return fadeTween.WaitForCompletion();
        _rotationTween.Kill();

        openWelcomeMenuEventSo.RaiseEvent();
        AudioManager.Instance.PlayMusic(AudioEnum.Main_Theme);
    }
}