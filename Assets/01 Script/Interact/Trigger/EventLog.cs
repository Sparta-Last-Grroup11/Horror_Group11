using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLog : Receiver
{
    [SerializeField] private List<int> commentID;
    [SerializeField] protected int questID = -1;
    public override void ReceiveTrigger()
    {
        foreach (int i in commentID)
        {
            MonologueManager.Instance.DialogPlay(i);
        }
        QuestManager.Instance.QuestTrigger(questID);
    }
}
