using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : OpenDoor,I_Interactable
{
    [SerializeField] private EItemID keyID;
    [SerializeField] private GetItemList itemList;

    public void OnInteraction()
    {
        OpenLockedDoor();
    }

    void OpenLockedDoor()
    {
        if (itemList == null)
        {
            Debug.LogWarning("itemList가 연결되지 않았습니다.");
            return;
        }

        if ( keyID != EItemID.None)
        {
            if (itemList.HaveItem(keyID))
            {
                OpenTheDoor();
            }
            else
            {
                Debug.Log("You don't have key");
            }
        }
        else
        {
            OpenTheDoor();
        }
    }
}
