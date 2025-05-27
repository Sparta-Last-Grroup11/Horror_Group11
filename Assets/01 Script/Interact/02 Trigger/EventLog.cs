using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLog : Receiver //퀘스트 진행용 리시버
{
    [SerializeField] protected int questID = -1;
    public override void ReceiveTrigger()
    {
        QuestManager.Instance.QuestTrigger(questID);
    }
}
