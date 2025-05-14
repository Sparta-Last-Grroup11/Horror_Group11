using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : Lever, I_Interactable
{
    [SerializeField] private SwitchPuzzle puzzle;

    public void OnInteraction() //레버 상호작용
    {
        isAct = true;
        StartCoroutine(Movelever(offRotation, onRotation));
        if (!puzzle.CheckCount()) //답이 아닐 경우
        {
            StartCoroutine(ReadyMove());
        }
        else //답일 경우
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
    IEnumerator ReadyMove() //레버 회전 대기 후 레버 회전
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
