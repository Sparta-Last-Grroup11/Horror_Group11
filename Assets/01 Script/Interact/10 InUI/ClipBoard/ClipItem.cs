using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Windows;

public class ClipItem : MonoBehaviour , I_Interactable
{
    [SerializeField] string description;
    public string Description { get { return description; } set { description = value; } }
    [SerializeField] GameObject ObjPrefabInUI;
    public void OnInteraction()
    {
        UIManager.Instance.show<UI3D>().Init(ObjPrefabInUI, description);
    }
}
