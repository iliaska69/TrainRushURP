using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeSliceInput : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO startLevelSOEvent;


    void Update()
    {
        if (Input.anyKey)
        {
            startLevelSOEvent.RaiseEvent();
        }
    }
}
