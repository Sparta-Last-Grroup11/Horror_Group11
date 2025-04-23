using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterEvent : Receiver
{
    private DoubleDoor door;
    [SerializeField] private ItemData changeKey;
    public override void ReceiveTrigger()
    {
        door = GetComponent<DoubleDoor>();
        Debug.Log(door.name);
        door.CloseBecauseEnter(changeKey);
    }
}
