using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class ClipItem : MonoBehaviour, I_Interactable
{
    [SerializeField] string description;

    TextMeshProUGUI text;

    public void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        text.text = description;
    }

    public void OnInteraction()
    {

    }
}
