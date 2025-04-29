using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLog : Receiver
{
    [SerializeField] private List<int> commentID;

    public override void ReceiveTrigger()
    {
        Debug.Log("a");
        foreach(int i in commentID)
        {
            MonologueManager.Instance.DialogPlay(commentID[i]);
        }
        gameObject.SetActive(false);
    }
}
