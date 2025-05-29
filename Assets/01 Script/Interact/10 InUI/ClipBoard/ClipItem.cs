using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Windows;

//클립 보드는 3D 상에서 각기 다른 텍스트를 표기해줘야 하기에 멤버 변수가 다름.
public class ClipItem : MonoBehaviour , I_Interactable
{
    [SerializeField] string description;
    public string Description { get { return description; } set { description = value; } }
    [SerializeField] GameObject ObjPrefabInUI;
    public void OnInteraction()
    {
        UIManager.Instance.show<UI3D>().Init(ObjPrefabInUI, description, UIManager.Instance.show<UI3DHelp>());
    }
}
