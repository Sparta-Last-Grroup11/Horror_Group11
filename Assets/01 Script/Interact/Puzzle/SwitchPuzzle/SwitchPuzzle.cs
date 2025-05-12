using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPuzzle : MonoBehaviour
{
    [SerializeField] private int[] startSwitchOn;
    [SerializeField] private int onCount;
    [SerializeField] private Light light;
    private Dictionary<int, Switch> switchesDict = new Dictionary<int, Switch>();
    public Action PuzzleClear; 

    private void Start()
    {
        onCount = 0;
        foreach(int id in startSwitchOn)
        {
            switchesDict[id].ChangeIsOn();
        }
        light.color = Color.red;
    }

    public void ChangeCount(int count)
    {
        onCount += count;
    }

    public void SetDictionary(int id,  Switch switchObj)
    {
        switchesDict[id] = switchObj;
    }

    public void TriggerSwitch(int id)
    {
        switchesDict[id].ChangeIsOn();
        if (id - 1 >= 0 && id % 3 != 0) switchesDict[id - 1].ChangeIsOn();
        if(id+1< switchesDict.Count && id%3 != 2) switchesDict[id+1].ChangeIsOn();
        if (id - 3 >= 0) switchesDict[id - 3].ChangeIsOn();
        if (id + 3 < switchesDict.Count) switchesDict[id + 3].ChangeIsOn();
    }

    public bool CheckCount()
    {
        if (onCount == 9)
        {
            light.color = Color.green;
            ClearPuzzle();
            return true;
        }
        else
        {
            light.color = Color.red;
            ResetAll();
            return false;
        }
    }

    private void ClearPuzzle()
    {
        foreach (Switch switchObj in switchesDict.Values)
        {
            switchObj.DownPuzzle();
        }
        PuzzleClear?.Invoke();
    }

    private void ResetAll()
    {
        foreach (Switch switchObj in switchesDict.Values)
        {
            switchObj.ResetSwitch();
        }
        foreach (int id in startSwitchOn)
        {
            switchesDict[id].ChangeIsOn();
        }
    }
}
