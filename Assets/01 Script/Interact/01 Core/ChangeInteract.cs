using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeInteract : MonoBehaviour //특정 퀘스트 도달 시 레이어 변경을 위한 코드
{
    [SerializeField] private int questID;
    void Start()
    {
        QuestManager.Instance.AddChanger(this, questID);
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void ChangeLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
}
