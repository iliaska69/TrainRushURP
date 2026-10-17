using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CrossfadeAnimation : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeDuration;

    public void StartCrossFadeAnimation()
    {
        StartCoroutine(Crossfade());
    }
    IEnumerator Crossfade()
    {
        gameObject.SetActive(true);
        canvasGroup.DOFade(1f, fadeDuration).SetEase(Ease.Linear);

        while (!LevelManager.instance.SceneLevelIsLoaded)
        {
            yield return null;
        }

        canvasGroup.DOFade(0f, fadeDuration).SetEase(Ease.Linear);
        gameObject.SetActive(false);
    }
}
