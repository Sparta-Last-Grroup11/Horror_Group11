using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPuzzle : MonoBehaviour
{
    [SerializeField]private Switch[] switches;
    [SerializeField] private int[] startSwitchOn;
    [SerializeField] private int onCount;
    private Dictionary<int, Switch> switchesDict = new Dictionary<int, Switch>();
    private void Start()
    {
        onCount = 0;
        foreach(int id in startSwitchOn)
        {
            switchesDict[id-1].ChangeIsOn();
        }
    }

    public void ChangeCount(int count)
    {
        onCount += count;
    }

    public void CheckCount()
    {
        if(onCount == 9)
        {
            Debug.Log("Puzzle is Clear");
            foreach(Switch switchObj in switchesDict.Values){
                switchObj.DownPuzzle();
            }
        }
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
}
