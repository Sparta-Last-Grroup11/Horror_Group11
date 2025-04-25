using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryItemInfo
{
    public ItemData ItemData;
    public int quantity;

    public InventoryItemInfo(ItemData itemData, int quantity)
    {
        this.ItemData = itemData;
        this.quantity = quantity;
    }
}

public class PlayerInventory
{
    Dictionary<string, InventoryItemInfo> inventory;
    public Dictionary<string, InventoryItemInfo> Inventory => inventory;

    public PlayerInventory()
    {
        inventory = new Dictionary<string, InventoryItemInfo>();
    }

    public void AddItem(ItemData data)
    {
        if (inventory.ContainsKey(data.name))
        {
            inventory[data.name].quantity++;
            return;
        }
        inventory.Add(data.name, new InventoryItemInfo(data, 1));
    }

    public void UseItem(ItemData data)
    {
        if (!inventory.ContainsKey(data.name))
            return;
        inventory[data.name].quantity--;
        if (inventory[data.name].quantity <= 0)
        {
            inventory.Remove(data.name);
        }
    }

    public bool HasItem(ItemData data)
    {
        return inventory.ContainsKey(data.name);
    }

    public bool HasItem(string name)
    {
        return inventory.ContainsKey(name);
    }

    public void ShowInventory()
    {
        UIManager.Instance.show<PopupInventoryUI>();
    }
}
