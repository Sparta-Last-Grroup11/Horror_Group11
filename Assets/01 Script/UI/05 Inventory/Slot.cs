using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//인벤토리용

public class Slot : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemCount;
    public string Description;
    public GameObject Object3D;

    public void Init(InventoryItemInfo item)
    {
        itemImage.sprite = item.ItemData.Icon;
        itemName.text = item.ItemData.ItemName;
        itemCount.text = item.quantity.ToString();
        Description = item.ItemData.Description;
        Object3D = item.ItemData.ObjectIn3D;
    }
}
