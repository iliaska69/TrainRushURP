using UnityEngine;

public class SpeedBonus : Bonus
{
    [SerializeField] private VoidEventChannelSO speedBonusEvent;
    [SerializeField] private SfxSound nitroSfx;

    [SerializeField] private bool _isActivate;

    public override void Activate()
    {
        if (_isActivate)
        {
            Debug.Log("ACTIVATE BONUS");
            if (nitroSfx != null)
                nitroSfx.Play();
            
            speedBonusEvent.RaiseEvent();
        }
        else
        {
            Deactivate();
        }
    }

    public override void Deactivate()
    {
        Debug.Log("Deactivate BONUS");
        speedBonusEvent.RaiseEvent();
    }
}