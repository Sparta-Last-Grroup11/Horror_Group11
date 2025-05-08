using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, I_Interactable
{
    [SerializeField] private bool isOn = false;
    [SerializeField] private Switch[] neighborSwitch;
    [SerializeField] private SwitchPuzzle puzzle;
    public void OnInteraction()
    {
        ChangeIsOn();
        foreach(var neighbor in neighborSwitch)
        {
            neighbor.ChangeIsOn();
        }
        puzzle.CheckCount();
    }

    public void ChangeIsOn()
    {
        isOn = !isOn;
        if (isOn)
        {
            puzzle.GetCount(1);
        }
        else
        {
            puzzle.GetCount(-1);
        }
    }

    public void DownPuzzle()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
}
