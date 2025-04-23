using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOn : MonoBehaviour, I_Interactable
{
    [SerializeField] private RoomsLightManage roomManage;
    [SerializeField] private float flash = 0.2f;
    [SerializeField] private bool isAct;
    public void OnInteraction()
    {
        if (isAct) return;
        isAct = true;
        PuzzleManager.Instance.OnPower();
        //StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        for (int i = 0; i < 2; i++)
        {
            roomManage.TurnBMLight();
            yield return new WaitForSeconds(flash++);
            roomManage.TurnBMLight();
            yield return new WaitForSeconds(flash++);
        }
    }
}
