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
        ItemData newItemData = Instantiate(itemData);
        newItemData.name = itemData.name;
        newItemData.ItemName = itemData.ItemName;
        newItemData.Description = itemData.Description;
        newItemData.ObjectIn3D = ResourceManager.Instance.Load<GameObject>(ResourceType.Item, this.name);
        GameManager.Instance.player.playerInventory.AddItem(newItemData);
        Destroy(gameObject);
    }
}
