using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecieveObserver : MonoBehaviour
{
    public EventTrigger eventTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SkinLess"))
        {
            eventTrigger.AddReceiver(other.GetComponent<EnemyReceiver>());
            other.GetComponent<EnemyReceiver>().SetEventTrigger(eventTrigger);
        }
    }
}
