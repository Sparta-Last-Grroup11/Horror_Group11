using System.Collections.Generic;
using UnityEngine;

public class StageTriggerController : Singleton<StageTriggerController>
{
    public List<GameObject> triggers;  // Trigger가 되는 콜라이더 오브젝트 목록
    public List<ZombieTriggerPair> activePairs = new();  // 실제로 사용할 좀비-트리거 연결 리스트

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

    public void ReceivePairs(List<ZombieTriggerPair> pairs)  // StageManager로부터 활성화할 트리거-좀비 쌍을 전달받음
    {
        activePairs = pairs;
    }

    public void ActivateTriggers()  // 선택된 트리거 오브젝트를 활성화함
    {
        foreach (var pair in activePairs)
        {
            if (pair.trigger != null)
            {
                pair.trigger.SetActive(true);
                var triggerEvent = pair.trigger.GetComponent<TriggerForEvent>();

            }

        }

    }
}

