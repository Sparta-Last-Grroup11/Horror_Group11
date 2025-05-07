using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterEvent : Receiver
{
    [SerializeField] private ItemData changeKey;
    [SerializeField] private int questID = 1;
    public override void ReceiveTrigger()
    {
        MonologueManager.Instance.DialogPlay(7);
        QuestManager.Instance.QuestTrigger(questID);
        GetComponent<DoubleDoor>().CloseBecauseEnter(changeKey);
    }
}
