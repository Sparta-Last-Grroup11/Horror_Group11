using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Switch : Lever, I_Interactable
{
    [SerializeField] private SwitchPuzzle puzzle;
    [SerializeField] private int id;

    protected override void Awake()
    {
        puzzle.SetDictionary(id, this);
        base.Awake();
    }
    public void OnInteraction()
    {
        isAct = true;
        puzzle.TriggerSwitch(id);
    }

    public void ChangeIsOn()
    {
        isOn = !isOn;
        if (isOn)
        {
            puzzle.ChangeCount(1);
            StartCoroutine(Movelever(offRotation, onRotation));
        }
        else
        {
            puzzle.ChangeCount(-1);
            StartCoroutine(Movelever(onRotation, offRotation));
        }
    }
    public void DownPuzzle()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void ResetSwitch()
    {
        if (isOn) ChangeIsOn();
    }
}
