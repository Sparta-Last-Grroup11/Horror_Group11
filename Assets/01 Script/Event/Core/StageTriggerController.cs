using System.Collections.Generic;
using UnityEngine;

public class StageTriggerController : Singleton<StageTriggerController>
{
    public List<GameObject> triggers;  // Trigger가 되는 각 콜라이더 오브젝트
    public List<ZombieTriggerPair> activePairs = new();

    private void Awake()
    {
        triggers = new List<GameObject>();

        foreach (var triggerEvent in GetComponentsInChildren<TriggerForEvent>(true))
        {
            var triggerObj = triggerEvent.gameObject;
            triggers.Add(triggerObj);
            triggerObj.SetActive(false);
        }
    }

    public void ReceivePairs(List<ZombieTriggerPair> pairs)
    {
        activePairs = pairs;
    }

    public void ActivateTriggers()
    {
        foreach (var pair in activePairs)
        {
            if (pair.trigger != null)
            {
                pair.trigger.SetActive(true);
                var triggerEvent = pair.trigger.GetComponent<TriggerForEvent>();
                if (triggerEvent != null && triggerEvent.receivers != null && triggerEvent.receivers.Count > 0)
                {
                    triggerEvent.receivers[0].gameObject.SetActive(true);
                }

            }

            if (pair.zombie != null)
            {
                pair.zombie.SetActive(true);
            }

        }

    }
}

