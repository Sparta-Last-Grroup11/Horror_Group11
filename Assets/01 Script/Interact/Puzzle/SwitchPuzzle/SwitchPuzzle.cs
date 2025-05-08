using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPuzzle : MonoBehaviour
{
    [SerializeField]private Switch[] switches;
    [SerializeField] private int[] startSwitchOn;
    [SerializeField] private int onCount;

    private void Start()
    {
        onCount = 0;
        foreach(int i in startSwitchOn)
        {
            switches[i-1].ChangeIsOn();
        }
    }

    public void GetCount(int count)
    {
        onCount += count;
    }

    public void CheckCount()
    {
        if(onCount == 9)
        {
            Debug.Log("Puzzle is Clear");
            foreach(Switch switchObj in switches){
                switchObj.DownPuzzle();
            }
        }
    }
}
