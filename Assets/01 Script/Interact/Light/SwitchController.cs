using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class SwitchController : MonoBehaviour, I_Interactable
{
    [SerializeField] private float intensity;
    [SerializeField] private float range;
    private HashSet<Lamp> lightsList = new HashSet<Lamp>();
    private bool isTurnOn = false;
    [SerializeField] private bool isPowerOn = false;
    [SerializeField] private int rand = 10;
    [SerializeField] private float shutdownTime = 90f;
    [SerializeField] private LightStateSO lightState;
    [SerializeField] private int questID = 5;
    public void AddLight(Lamp lamp) //목록에 lamp 추가
    {
        lightsList.Add(lamp);
        lamp.SetLight(intensity, range, rand);
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
            MonologueManager.Instance.DialogPlay(13);
        }
        else
        {
            isTurnOn = false;
            foreach (Lamp lamp in lightsList)
            {
                lamp.TurnOff();
            }
            MonologueManager.Instance.DialogPlay(1);
        }
        if (lightState != null)
        {
            lightState.SetLightState(isTurnOn);
        }
    }

    public void OnInteraction() //상호작용
    {
        if (!isPowerOn || isTurnOn) return;
        SetLightsState();
        QuestManager.Instance.QuestTrigger(questID);
        StartCoroutine(Shutdown());
    }

    public void OnPower() //전원 전환
    {
        isPowerOn = true;
    }

    IEnumerator Shutdown()
    {
        yield return new WaitForSeconds(shutdownTime);
        SetLightsState();
    }
}
