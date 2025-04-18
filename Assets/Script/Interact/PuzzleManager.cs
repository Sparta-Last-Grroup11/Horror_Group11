using System;
using Unity;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : Singleton<PuzzleManager>
{
    public OpenPuzzle GateHouseSaveDial;
    [SerializeField] private bool isPowerOn;

    public void OnPower()
    {
        isPowerOn = true;
    }

    public bool GetIsPowerOn()
    {
        return isPowerOn;
    }
}
