using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LightStateSO", menuName = "new LightStateSO")]
public class LightStateSO : ScriptableObject
{
    private bool isPowerOn = false; //전원이 켜졌는지
    public bool IsPowerOn => isPowerOn;

    private bool canControl = false; //스위치 퍼즐을 풀어 조작이 가능한지
    public bool CanControl => canControl;

    private bool isLightOn = false; //전등이 켜진 상태인지
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
    public void OnPower()
    {
        isPowerOn = true;
        GameManager.Instance.CheckPointSave(new Vector3(-3.95f, 5.88f, 17.70f));
    }

    public void PermissionControl()
    {
        canControl = true;
    }
    public void ResetLight()
    {
        isPowerOn = false;
        isLightOn = false;
        canControl = false;
        OnLightStateChanged = null;
    }
}
