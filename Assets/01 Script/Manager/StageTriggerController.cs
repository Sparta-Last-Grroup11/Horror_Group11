using System.Collections.Generic;
using UnityEngine;

public class StageTriggerController : Singleton<StageTriggerController>
{
    [SerializeField] private List<GameObject> observers;  // Trigger가 되는 각 콜라이더 오브젝트
    [SerializeField] private List<Receiver> receivers;

    public void SetupReceivers()
    {
        receivers.Clear();

        Transform triggerGroup = GameObject.Find("TriggerGroup")?.transform;
        if (triggerGroup == null )
        {
            Debug.LogWarning("[StageTriggerController] not Found TriggerGroup");
            return;
        }
        foreach(Transform triggerZone in triggerGroup)
        {
            EventTrigger eventTrigger = triggerZone.GetComponentInChildren<EventTrigger>(true);
            EnemyReceiver[] enemyReceivers = triggerZone.GetComponentsInChildren<EnemyReceiver>(true);
            foreach (EnemyReceiver enemyReceiver in enemyReceivers)
            {
                if(enemyReceiver != null)
                {
                    enemyReceiver.SetEventTrigger(eventTrigger);
                    eventTrigger.AddReceiver(enemyReceiver);
                    receivers.Add(enemyReceiver);
                }
            }
        }
    }


    /// <summary>
    /// StageManager가 호출해서 필요한 트리거만 활성화시키는 메서드
    /// </summary>
    /// <param name="activeIndices">활성화할 오브젝트 인덱스 리스트</param>
    public void ActivateTriggers(List<int> activeIndices)
    {
        if (activeIndices == null)
        {
            Debug.LogError("[StageTriggerController] 활성화할 인덱스 리스트가 null입니다.");
            return;
        }

        for (int i = 0; i < observers.Count; i++)
        {
            bool isActive = activeIndices.Contains(i);
            if (i < observers.Count && observers[i] != null)
                observers[i].SetActive(isActive);

            if(i < receivers.Count && receivers[i] != null)
            {
                var enemyReceiver = receivers[i] as EnemyReceiver;
                if (enemyReceiver != null && enemyReceiver.gameObject != null)
                {
                    enemyReceiver.gameObject.SetActive(isActive);
                }
            }
        }
    }

    public void DeactivateUnregisteredReceivers()
    {
        foreach (var observer in observers)
        {
            var recieveObserver = observer.GetComponent<RecieveObserver>();
            if (recieveObserver != null && !recieveObserver.IsRegistered())
            {
                EnemyReceiver enemyReceiver = observer.GetComponentInChildren<EnemyReceiver>();
                if (enemyReceiver != null)
                {
                    enemyReceiver.gameObject.SetActive(false);
                }
            }
        }
    }
}

