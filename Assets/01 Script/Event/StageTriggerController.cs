using System.Collections.Generic;
using UnityEngine;

public class StageTriggerController : Singleton<StageTriggerController>
{
    public List<GameObject> triggers;  // Trigger가 되는 각 콜라이더 오브젝트
    // private List<int> currentIndices = new List<int>();
    public List<ZombieTriggerPair> activePairs = new();

    private void Awake()
    {
        foreach (var trigger in triggers)
        {
            if (trigger != null)
                trigger.SetActive(false);
        }
    }

    public void ReceivePairs(List<ZombieTriggerPair> pairs)
    {
        activePairs = pairs;
    }

    //public void GetTriggers(List<int> activeIndices)
    //{
    //    if (activeIndices == null)
    //    {
    //        Debug.LogError("[StageTriggerController] 활성화할 인덱스 리스트가 null입니다.");
    //        return;
    //    }

    //    currentIndices.Clear();
    //    currentIndices = activeIndices;
    //}

    public void ActivateTriggers()
    {
        foreach (var pair in activePairs)
        {
            if (pair.trigger != null)
            {
                pair.trigger.SetActive(true);
                var triggerEvent = pair.trigger.GetComponentInChildren<TriggerForEvent>();
                if (triggerEvent != null && triggerEvent.receivers.Count > 0)
                {
                    triggerEvent.receivers[0].gameObject.SetActive(true);
                }

            }

            if (pair.zombie != null)
            {
                pair.zombie.SetActive(true);
            }

        }

        //for (int i = 0; i < triggers.Count; i++)
        //{
        //    bool isActive = currentIndices.Contains(i);

        //    if (i < triggers.Count && triggers[i] != null)
        //    {
        //        triggers[i].SetActive(isActive);
        //        if (isActive == true)
        //        {
        //            triggers[i].transform.GetComponentInChildren<TriggerForEvent>().receivers[0].gameObject.SetActive(isActive);
        //        }
        //    }

        //}
    }
}

