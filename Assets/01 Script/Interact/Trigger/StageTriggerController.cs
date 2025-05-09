using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTriggerController : Singleton<StageTriggerController>
{
    [SerializeField] private List<GameObject> observers;  // Trigger가 되는 각 콜라이더 오브젝트

    private List<int> currentIndices = new List<int>();
    private bool isPlayerOn;

    public void GetTriggers(List<int> activeIndices)
    {
        if (activeIndices == null)
        {
            Debug.LogError("[StageTriggerController] 활성화할 인덱스 리스트가 null입니다.");
            return;
        }

        currentIndices.Clear();
        currentIndices = activeIndices;
    }

    public void ActivateTriggers()
    {
        for (int i = 0; i < observers.Count; i++)
        {
            bool isActive = currentIndices.Contains(i);

            if (i < observers.Count && observers[i] != null)
            {
                Debug.Log("dd");
                observers[i].SetActive(isActive);
                observers[i].transform.GetComponentInChildren<TriggerForEvent>().receivers[0].gameObject.SetActive(isActive);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null && isPlayerOn == false)
        {
            isPlayerOn = true;
            ActivateTriggers();
        }
    }

}

