using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ReceiverExit : Receiver
{
    [SerializeField] private bool testShowClearUI = false; // 디버그용

    [SerializeField] private ItemData exitItem;
    private Vector3 origin;

    private void Start()
    {
        if (testShowClearUI)
        {
            ReceiveTrigger();
        }
    }

    public override void ReceiveTrigger()
    {
        if (exitItem != null && GameManager.Instance.player.playerInventory.HasItem(exitItem)){
            MonologueManager.Instance.DialogPlay(8);
            UIManager.Instance.show<EndGameUI>().ShowEnding(EndingCategory.Escape, 1000);
        }
        else
        {
            StartCoroutine(Ready());
        }
    }

    IEnumerator Ready()
    {
        yield return new WaitForSeconds(10f);
        eventTrigger.gameObject.SetActive(true);
    }
}
