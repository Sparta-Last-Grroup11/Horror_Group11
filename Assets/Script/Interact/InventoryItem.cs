using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public ItemData itemData;
    public int count;

    public InventoryItem(ItemData itemData, int count)
    {
        this.itemData = itemData;
        this.count = count;
    }
}
