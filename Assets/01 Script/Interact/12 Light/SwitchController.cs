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

    [SerializeField] private int rand = 10;
    [SerializeField] private float shutdownTime = 90f;
    [SerializeField] private LightStateSO lightState;
    [SerializeField] private int questID = 5;

    [SerializeField] private Telephone telephone;

    [Header("Spakle")]
    [SerializeField] private GameObject spark;

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
            spark.SetActive(true);
        }
        else
        {
            isTurnOn = false;
            foreach (Lamp lamp in lightsList)
            {
                lamp.TurnOff();
            }
            MonologueManager.Instance.DialogPlay(1);
            spark.SetActive(false);
        }
        if (lightState != null)
        {
            lightState.SetLightState(isTurnOn);
        }
    }

    public void OnInteraction() //상호작용
    {
        if (!lightState.CanControl || isTurnOn) return;
        SetLightsState();
        telephone.OnPower();
        QuestManager.Instance.QuestTrigger(questID);
        StartCoroutine(Shutdown());
    }

    IEnumerator Shutdown()
    {
        yield return new WaitForSeconds(shutdownTime);
        SetLightsState();
    }
}
