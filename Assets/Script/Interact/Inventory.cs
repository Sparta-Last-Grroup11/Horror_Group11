using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [SerializeField] private Dictionary<ItemData, int> itemList;

    protected override void Awake()
    {
        base.Awake();
        itemList = new Dictionary<ItemData, int>();

    }
    public void GetItem(ItemData item, int num = 1)
    {
        if (itemList.ContainsKey(item))
        {
            itemList[item] += num;
        }
        else
        {
            itemList.Add(item, num);
        }
    }
    public bool UseItem(ItemData item, int num)
    {
        if (itemList.ContainsKey(item) && itemList[item] >= num)
        {
            itemList[item]-= num;
            if (itemList[item] == 0)
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
