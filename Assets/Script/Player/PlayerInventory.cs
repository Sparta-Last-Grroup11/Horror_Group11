using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory
{
    Dictionary<string, ItemData> inventory;
    public Dictionary<string, ItemData> Inventory => inventory;

    public PlayerInventory()
    {
        inventory = new Dictionary<string, ItemData>();
    }

    public void AddItem(ItemData data)
    {
        inventory[data.name] = data;
    }

    public void RemoveItem(ItemData data)
    {
        inventory.Remove(data.name);
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
