using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Receiver : MonoBehaviour
{
    [SerializeField] private EventTrigger eventTrigger;

    private void Awake()
    {
        eventTrigger.AddReceiver(this);
    }
    public abstract void ReceiveTrigger();
}
