using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkinLessZombieTriggerReceiver : MonoBehaviour
{
    public EventTrigger eventTrigger;
    private bool isZombieOn;

    private void Awake()
    {
        if (eventTrigger == null)
            eventTrigger = GetComponentInParent<EventTrigger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SkinLess"))
        {
            var enemyReceiver = other.GetComponent<JumpScaleReceiver>();
            if (enemyReceiver != null)
            {
                eventTrigger.AddReceiver(enemyReceiver);
                enemyReceiver.SetEventTrigger(eventTrigger);
            }
            if (isZombieOn == false)
            {
                isZombieOn = true;
                enemyReceiver.gameObject.SetActive(false);
                eventTrigger.transform.parent.gameObject.SetActive(false);

            }

        }
    }

}
