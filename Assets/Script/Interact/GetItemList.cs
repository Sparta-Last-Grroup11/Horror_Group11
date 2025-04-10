using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemList : MonoBehaviour
{
    [SerializeField] private Dictionary<EItemID, int> itemList;

    private void Awake()
    {
        itemList = new Dictionary<EItemID, int>();

    }
    public void GetItem(EItemID itemId, int num)
    {
        if (itemList.ContainsKey(itemId))
        {
            itemList[itemId] += num;
        }
        else
        {
            itemList.Add(itemId, num);
        }
    }
    public bool UseItem(EItemID itemId, int num = 1)
    {
        if (itemList.ContainsKey(itemId) && itemList[itemId] >= num)
        {
            itemList[itemId]-= num;
            if (itemList[itemId] == 0)
            {
                itemList.Remove(itemId);
            }
            return true;
        }
        else
        {
            Debug.Log("아이템의 갯수가 부족하거나 존재하지 않습니다.");
            return false;
        }
        
    }
    public bool HaveItem(EItemID itemId)
    {
        return itemList.ContainsKey(itemId);
    }
}
