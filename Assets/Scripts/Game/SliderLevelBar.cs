using UnityEngine;

public class SliderLevelBar:  MonoBehaviour
{
    public static SliderLevelBar instance;

    private float _sliderValue;
    private bool _isInit;
    private Railway _railway;
    
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

    private void Update()
    {
        if (_railway == null) _isInit = false;
        if (Game.instance?.GetRailway() == null || _isInit) return;
        _railway = Game.instance?.GetRailway();
        _isInit = true;
    }

    public float GetSliderValue() => _sliderValue;

    public void SetSliderPercentByObjectId(int objectId)
    {
        if (_railway == null) return;
        _sliderValue = _railway.GetPercentByObjectId(objectId);
    }
}