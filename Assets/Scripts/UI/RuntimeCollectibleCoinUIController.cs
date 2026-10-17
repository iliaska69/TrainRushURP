using DG.Tweening;
using TMPro;
using UnityEngine;

public class RuntimeCollectibleCoinUIController : MonoBehaviour
{
    [SerializeField] private IntEventChannelSO collectibleCoinCollectedEvent;
    [SerializeField] private RectTransform popupPrefab;
    [SerializeField] private RectTransform popupParent;
    [SerializeField] private float floatDistance = 120f;
    [SerializeField] private float animationDuration = 1.2f;
    [SerializeField] private float horizontalSpread = 40f;

    private Vector2 _spawnAnchoredPosition;

    private void Awake()
    {
        if (popupPrefab == null)
            return;

        _spawnAnchoredPosition = popupPrefab.anchoredPosition;
        popupPrefab.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (collectibleCoinCollectedEvent != null)
            collectibleCoinCollectedEvent.OnEventRaised += OnCoinCollected;
    }

    private void OnDisable()
    {
        if (collectibleCoinCollectedEvent != null)
            collectibleCoinCollectedEvent.OnEventRaised -= OnCoinCollected;
    }

    private void OnCoinCollected(int coinValue)
    {
        if (popupPrefab == null || popupParent == null)
            return;

        var instance = Instantiate(popupPrefab, popupParent);
        instance.gameObject.SetActive(true);

        var offsetX = Random.Range(-horizontalSpread, horizontalSpread);
        instance.anchoredPosition = _spawnAnchoredPosition + new Vector2(offsetX, 0f);

        var text = instance.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null)
            text.text = "+" + coinValue;

        var canvasGroup = instance.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = instance.gameObject.AddComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;

        var startY = instance.anchoredPosition.y;
        var targetY = startY + floatDistance;
        var moveDuration = animationDuration;
        var fadeInDuration = moveDuration * 0.15f;
        var fadeOutStart = moveDuration * 0.3f;
        var fadeOutDuration = moveDuration - fadeOutStart;

        DOTween.Sequence()
            .Append(canvasGroup.DOFade(1f, fadeInDuration).SetEase(Ease.OutQuad))
            .Join(instance.DOAnchorPosY(targetY, moveDuration).SetEase(Ease.OutQuad))
            .Insert(fadeOutStart, canvasGroup.DOFade(0f, fadeOutDuration).SetEase(Ease.InQuad))
            .OnComplete(() => Destroy(instance.gameObject));
    }
}
