using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ReceiverExit : Receiver
{
    [SerializeField] private ItemData exitItem;
    private Vector3 origin;
    public override void ReceiveTrigger()
    {
        if (exitItem != null && GameManager.Instance.player.playerInventory.HasItem(exitItem)){
            MonologueManager.Instance.DialogPlay(8);
            UIManager.Instance.show<ClearUI>();
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
