using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeInteract : MonoBehaviour
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
