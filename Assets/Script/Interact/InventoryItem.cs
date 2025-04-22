using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryItem : MonoBehaviour, I_Interactable
{
    [SerializeField] ItemData itemData;
    public ItemData ItemData => itemData;

    public virtual void OnInteraction()
    {
        GameManager.Instance.player.playerInventory.AddItem(itemData);
        Destroy(gameObject);
    }
}
