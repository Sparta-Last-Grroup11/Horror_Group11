using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterEvent : Receiver
{
    [SerializeField] private ItemData changeKey;

    public override void ReceiveTrigger()
    {
        MonologueManager.Instance.DialogPlay(7);
        GetComponent<DoubleDoor>().CloseBecauseEnter(changeKey);
    }
}
