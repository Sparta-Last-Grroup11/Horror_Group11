using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SwitchPuzzle : MonoBehaviour
{
    [SerializeField] private int[] startSwitchOn;
    [SerializeField] private int onCount;
    [SerializeField] private Light switchLight;
    private Dictionary<int, Switch> switchesDict = new Dictionary<int, Switch>();
    public event Action PuzzleClear;
    [SerializeField] private LightStateSO lightState;
    private void Start()
    {
        onCount = 0;
        foreach(int id in startSwitchOn)
        {
            switchesDict[id].SetStartOnOff(true);
        }
        switchLight.color = Color.red;
    }

    public void ChangeCount(int count) //레버 작동 시 카운트 변경
    {
        onCount += count;
        if(onCount == 9)
        {
            switchLight.color = Color.yellow;
        }
    }

    public void SetDictionary(int id,  Switch switchObj) //리스트에 스위치, id 저장
    {
        switchesDict[id] = switchObj;
    }

    public void TriggerSwitch(int id) //스위치 작동 시 주변 스위치 On/Off
    {
        switchesDict[id].ChangeIsOn();
        if (id - 1 >= 0 && id % 3 != 0) switchesDict[id - 1].ChangeIsOn();
        if(id+1< switchesDict.Count && id%3 != 2) switchesDict[id+1].ChangeIsOn();
        if (id - 3 >= 0) switchesDict[id - 3].ChangeIsOn();
        if (id + 3 < switchesDict.Count) switchesDict[id + 3].ChangeIsOn();
    }

    public bool CheckCount() //레버가 전부 켜졌는지 확인
    {
        if (!lightState.IsPowerOn) return false;
        if (onCount == 9)
        {
            switchLight.color = Color.green;
            ClearPuzzle();
            return true;
        }
        else
        {
            switchLight.color = Color.red;
            ResetAll();
            return false;
        }
    }

    private void ClearPuzzle() //퍼즐 완료 시 레버 비활성화 및 클리어 시 전등 스위치 조작 가능
    {
        foreach (Switch switchObj in switchesDict.Values)
        {
            switchObj.DownPuzzle();
        }
        lightState.PermissionControl();
    }

    private void ResetAll() //모든 레버 초기화
    {
        foreach (Switch switchObj in switchesDict.Values)
        {
            switchObj.ResetSwitch();
        }
    }
}
