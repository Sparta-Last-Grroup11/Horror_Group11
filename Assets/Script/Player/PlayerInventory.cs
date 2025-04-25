using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        if (inventory.ContainsKey(data.name))
        {
            inventory[data.name].count++;
        }
        inventory[data.name] = data;
    }

    public void UseItem(ItemData data)
    {
        inventory[data.name].count--;
        if (inventory[data.name].count <= 0) 
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
