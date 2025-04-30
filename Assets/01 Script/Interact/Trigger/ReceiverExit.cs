using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReceiverExit : Receiver
{
    [SerializeField] private ItemData exitItem;

    public override void ReceiveTrigger()
    {
        if (exitItem != null && GameManager.Instance.player.playerInventory.HasItem(exitItem)){
            UIManager.Instance.show<ClearUI>();
            base.ReceiveTrigger();
        }
    }
}
