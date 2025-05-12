using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour, I_Interactable
{
    [SerializeField] private SwitchPuzzle puzzle;

    public void OnInteraction()
    {
        puzzle.CheckCount();
    }
}
