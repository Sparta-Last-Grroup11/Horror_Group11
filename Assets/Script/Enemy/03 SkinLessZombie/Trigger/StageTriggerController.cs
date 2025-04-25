using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using UnityEngine.EventSystems;

public class StageTriggerController : MonoBehaviour
{
    [SerializeField] private List<GameObject> observers;  // Trigger가 되는 각 콜라이더 오브젝트
    [SerializeField] private List<EventTrigger> eventTriggers;
    [SerializeField] private TextAsset triggerJson;  // 스테이지별로 활성화할 오브젝트 인덱스를 담는 테스트용 json 파일
    private List<TriggerGroupData> triggerGroups;
    private int currentStage;

    private void Start()
    {
        currentStage = StageNum.StageNumber;  
        triggerGroups = JsonConvert.DeserializeObject<List<TriggerGroupData>>(triggerJson.text);

        // 나중에 json 파일에서 각 스테이지마다 활성할 인덱스를 받아오는 구조입니다.
        foreach (var group in triggerGroups)
        {
            if (group.stage == currentStage)
            {
                for (int i = 0; i < observers.Count; i++)
                {
                    observers[i].SetActive(group.activeIndices.Contains(i));

                }
            }
        }

    }

}

