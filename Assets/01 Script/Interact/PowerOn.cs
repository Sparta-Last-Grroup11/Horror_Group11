using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOn : MonoBehaviour, I_Interactable
{
    [SerializeField] private bool isAct;
    [SerializeField] private SwitchController elecBox;
    [SerializeField] private AudioClip clip;
    public void OnInteraction()
    {
        if (isAct) return;
        isAct = true;
        if(clip != null)
            AudioManager.Instance.Audio2DPlay(clip);
        elecBox.OnPower();
        MonologueManager.Instance.DialogPlay(10);
        MonologueManager.Instance.DialogPlay(9);
    }
}
