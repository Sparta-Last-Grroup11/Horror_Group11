using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupInventoryUI : PopupUI
{
    [SerializeField] private TextMeshProUGUI inventoryTitle;
    [SerializeField] private GameObject itemSlotGroup;
    [SerializeField] private List<GameObject> slots; // 갖고 있는 아이템으로 변경 필요
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Image manualImage; // 조작법 이미지

    private void Start()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].transform.SetParent(itemSlotGroup.transform);
            slots[i].transform.SetAsLastSibling();
        }
    }
}
