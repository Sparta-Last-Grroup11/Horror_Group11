using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Windows;

public class ClipItem : MonoBehaviour, I_Interactable
{
    [SerializeField] string description;
    [SerializeField] GameObject ObjPrefabInUI;
    public void OnInteraction()
    {
        UIManager.Instance.UI3DManager.Open3DUI(ObjPrefabInUI, description);
    }
}
