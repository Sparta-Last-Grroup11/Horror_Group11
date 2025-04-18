using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [SerializeField] private Dictionary<ItemData, InventoryItem> itemList;

    protected override void Awake()
    {
        base.Awake();
        itemList = new Dictionary<ItemData, InventoryItem>();

    }
    public void GetItem(ItemData item, int num = 1)
    {
        if (itemList.ContainsKey(item))
        {
            itemList[item].count += num;
        }
        else
        {
            itemList.Add(item, new InventoryItem(item, num));
        }
    }
    public bool ConsumeItem(ItemData item, int num = 0)
    {
        if (itemList.ContainsKey(item) && itemList[item].count >= num)
        {
            itemList[item].count -= num;
            if (itemList[item].count == 0)
            {
                itemList.Remove(item);
            }
            return true;
        }
        else
        {
            Debug.Log("아이템의 갯수가 부족하거나 존재하지 않습니다.");
            return false;
        }
        
    }
    public bool HaveItem(ItemData item)
    {
        return itemList.ContainsKey(item);
    }
}
