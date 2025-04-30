using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLog : Receiver
{
    [SerializeField] private List<int> commentID;

    public override void ReceiveTrigger()
    {
        foreach (int i in commentID)
        {
            MonologueManager.Instance.DialogPlay(i);
        }
        base.ReceiveTrigger();
    }
}
