using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecieveObserver : MonoBehaviour
{
    public EventTrigger eventTrigger;
    private EnemyReceiver registeredReceiver;

    private void Awake()
    {
        if (eventTrigger == null)
            eventTrigger = GetComponentInParent<EventTrigger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SkinLess"))
        {
            var enemyReceiver = other.GetComponent<EnemyReceiver>();
            if (enemyReceiver != null)
            {
                eventTrigger.AddReceiver(enemyReceiver);
                enemyReceiver.SetEventTrigger(eventTrigger);
                registeredReceiver = enemyReceiver;
            }
        }
    }

    public bool IsRegistered()
    {
        return registeredReceiver != null;
    }
}
