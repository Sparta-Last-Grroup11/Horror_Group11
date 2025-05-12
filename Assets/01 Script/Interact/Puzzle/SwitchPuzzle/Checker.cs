using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : Lever, I_Interactable
{
    [SerializeField] private SwitchPuzzle puzzle;

    public void OnInteraction()
    {
        isAct = true;
        StartCoroutine(Movelever(offRotation, onRotation));
        if (!puzzle.CheckCount())
        {
            StartCoroutine(ReadyMove());
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
    IEnumerator ReadyMove()
    {
        while (isAct)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        isAct = true;
        StartCoroutine(Movelever(onRotation, offRotation));
    }
}
