using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, I_Interactable
{
    [SerializeField] private bool isOn = false;
    [SerializeField] private SwitchPuzzle puzzle;
    [SerializeField] private int id;
    private void Awake()
    {
        puzzle.SetDictionary(id, this);
    }
    public void OnInteraction()
    {
        puzzle.TriggerSwitch(id);
        puzzle.CheckCount();
    }

    public void ChangeIsOn()
    {
        isOn = !isOn;
        if (isOn)
        {
            puzzle.ChangeCount(1);
        }
        else
        {
            puzzle.ChangeCount(-1);
        }
    }

    public void DownPuzzle()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
}
