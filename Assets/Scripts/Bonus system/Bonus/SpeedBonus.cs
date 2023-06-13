using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBonus : Bonus
{
    [SerializeField] float speed;
    [SerializeField] private FloatEventChannelSO addSpeedBonus;


    public override void Activate()
    {
        addSpeedBonus.RaiseEvent(speed);
    }

    public override void Deactivate()
    {
        speed = -speed;
        addSpeedBonus.RaiseEvent(speed);
    }
}
