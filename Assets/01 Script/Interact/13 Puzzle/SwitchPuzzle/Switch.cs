using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Switch : Lever, I_Interactable
{
    [SerializeField] private SwitchPuzzle puzzle;
    [SerializeField] private int id;
    private bool startSetOn = false;
    protected override void Awake()
    {
        puzzle.SetDictionary(id, this);
        base.Awake();
    }
    public void SetStartOnOff(bool startSet) //시작부터 켜질 레버 설정
    {
        startSetOn = startSet;
        ChangeIsOn();
    }
    public void OnInteraction() 
    {
        isAct = true;
        puzzle.TriggerSwitch(id);
    }

    public void ChangeIsOn() //레버 On/Off
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
    public void DownPuzzle() //레버 상호작용 비활성화
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
    
    public void ResetSwitch() //레버 초기화
    {
        if (isOn != startSetOn) ChangeIsOn();
    }
}
