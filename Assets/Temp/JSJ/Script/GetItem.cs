using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour, I_Interactable
{
    public void OnInteraction()
    {
        // 전원실 키 획득
        gameObject.SetActive(false);
    }
}
