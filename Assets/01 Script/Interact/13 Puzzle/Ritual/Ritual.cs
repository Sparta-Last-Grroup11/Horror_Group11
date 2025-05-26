using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ritual : MonoBehaviour, I_Interactable
{
    [SerializeField] private GameObject key;
    [SerializeField] private int questID= 11;
    private int countActive = 0;
    private void Awake()
    {
        key.SetActive(false);
    }
    public void OnInteraction()
    {
        if (countActive == 5) //카운트가 5가 되었을 때 상호작용 시
        {
            key.SetActive(true);
            countActive = 0;
            QuestManager.Instance.QuestTrigger(questID);
        }
    }

    public void DoOffer() //촛불을 밝혔을 경우 카운트 증가
    {
        countActive++;
    }
}
