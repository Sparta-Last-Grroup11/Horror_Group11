using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOn : MonoBehaviour, I_Interactable
{
    public void OnInteraction()
    {
        PuzzleManager.Instance.OnPower();
    }
}
