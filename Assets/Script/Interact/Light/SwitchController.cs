using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class SwitchController : MonoBehaviour, I_Interactable
{
    [SerializeField] private float intensity;
    [SerializeField] private float range;
    [SerializeField] private List<Lamp> lightsList = new List<Lamp>();
    private bool isTurnOn = false;
    [SerializeField] private bool isPowerOn = false;
    [SerializeField] private int ran = 10;

    public void AddLight(Lamp lamp) //목록에 lamp 추가
    {
        lightsList.Add(lamp);
        lamp.SetLight(intensity, range, ran);
    }

    public void SetLightsState() //lamp의 불 켜기
    {
        if (!isTurnOn)
        {
            isTurnOn = true;
            foreach (Lamp lamp in lightsList)
            {
                lamp.TurnOn();
            }
        }
        else
        {
            isTurnOn = false;
            foreach (Lamp lamp in lightsList)
            {
                lamp.TurnOff();
            }
        }
    }

    public void OnInteraction() //상호작용
    {
        if (!isPowerOn || isTurnOn) return;
        SetLightsState();
    }

    public void OnPower() //전원 전환
    {
        isPowerOn = true;
    }
}
