using System.Collections.Generic;
using UnityEngine;

public class StageTriggerController : Singleton<StageTriggerController>
{
    [SerializeField] private List<GameObject> observers;  // Trigger가 되는 각 콜라이더 오브젝트

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
            observers[i].SetActive(activeIndices.Contains(i));
        }
    }
}

