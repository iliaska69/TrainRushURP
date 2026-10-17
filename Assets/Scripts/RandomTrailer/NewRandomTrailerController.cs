using UnityEngine;

public class NewRandomTrailerController : MonoBehaviour
{
    public static NewRandomTrailerController instance;

    [SerializeField] private int _countOfTrailerToTakeNewRandom;
    private float _percentOfNewRandomTrailer;
    private float _previousPercentOfNewRandomTrailer;
    private float _oneTrailerPercent;
    private bool _newTrailerReady;
    private const string PERCENT_OF_NEW_TRAILER = "Percent of new trailer";


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayerPrefs.DeleteKey(PERCENT_OF_NEW_TRAILER);
        _percentOfNewRandomTrailer = PlayerPrefs.GetFloat(PERCENT_OF_NEW_TRAILER);
        _oneTrailerPercent = 1f / _countOfTrailerToTakeNewRandom;
    }

    public float GetPercents() => _percentOfNewRandomTrailer;
    public void SetPercents(float value) => _percentOfNewRandomTrailer = value;
    public float GetPreviousPercents() => _previousPercentOfNewRandomTrailer;

    public void IncrementPercentOfNewRandomTrailer(int countOfNewTrailers)
    {
        var percents = _oneTrailerPercent * countOfNewTrailers;
        _previousPercentOfNewRandomTrailer = _percentOfNewRandomTrailer;
        _percentOfNewRandomTrailer += percents;
        PlayerPrefs.SetFloat(PERCENT_OF_NEW_TRAILER, _percentOfNewRandomTrailer);
        
        if (_percentOfNewRandomTrailer < 0.99f) return;
        _newTrailerReady = true;
    }

    public ShopItem TakeNewRandomTrailer()
    {
        if (!_newTrailerReady) return null;

        PlayerPrefs.SetFloat(PERCENT_OF_NEW_TRAILER, 0);
        _newTrailerReady = false;
        _previousPercentOfNewRandomTrailer = 0;

        var trailers = Shop.instance.GetUnpurchasedTrailers();
        if (trailers.Count == 0) return null;

        var randomTrailerIndex = Random.Range(0, trailers.Count);
        return trailers[randomTrailerIndex];
    }
}