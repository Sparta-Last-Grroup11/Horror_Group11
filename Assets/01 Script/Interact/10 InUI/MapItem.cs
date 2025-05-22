using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapItem : MonoBehaviour, I_Interactable
{
    public void OnInteraction()
    {
        UIManager.Instance.show<UI3D>().Init(gameObject, null, UIManager.Instance.show<UI3DHelp>(), true, 2);
    }
}
