using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour, I_Interactable
{
    [SerializeField] private ItemData item;
    public void OnInteraction()
    {
        GameManager.Instance.player.playerInventory.AddItem(item);
        Destroy(gameObject);
    }
}
