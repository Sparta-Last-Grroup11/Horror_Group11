using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LightStateSO", menuName = "new LightStateSO")]
public class LightStateSO : ScriptableObject
{
    private bool isLightOn = false;
    public bool IsLightOn => isLightOn;
    public event Action<bool> OnLightStateChanged;

    public void SetLightState(bool on)
    {
        if (isLightOn != on)
        {
            isLightOn = on;
            OnLightStateChanged?.Invoke(isLightOn);
        }
    }
}
