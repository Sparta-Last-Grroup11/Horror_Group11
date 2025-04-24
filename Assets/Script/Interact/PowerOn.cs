using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOn : MonoBehaviour, I_Interactable
{
    [SerializeField] private float flash = 0.2f;
    [SerializeField] private bool isAct;
    [SerializeField] private TurnLight lights;
    public void OnInteraction()
    {
        if (isAct) return;
        isAct = true;
        lights.OnPower();
    }
}
