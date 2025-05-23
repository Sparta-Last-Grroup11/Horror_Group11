using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLog : Receiver
{
    [SerializeField] protected int questID = -1;
    public override void ReceiveTrigger()
    {
        QuestManager.Instance.QuestTrigger(questID);
    }
}
