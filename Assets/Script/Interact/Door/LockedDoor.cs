using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LockedDoor : ControlDoor,I_Interactable
{
    [SerializeField] private EItemID keyID;
    [SerializeField] private GetItemList itemList;
    private bool isOpened;
    public void OnInteraction()
    {
        OpenLockedDoor();
    }

    void OpenLockedDoor()
    {
/*        if (itemList == null)
        {
            Debug.LogWarning("itemList가 연결되지 않았습니다.");
            return;
        }
*/
        if ( keyID != EItemID.None && !itemList.HaveItem(keyID))
        {
                Debug.Log("You don't have key");
                return;
        }
        else
        {
            OpenCloseDoor();
        }
    }

    void OpenCloseDoor()
    {
        if (isOpened)
        {
            OpenTheDoor();
            isOpened = false;
        }
        else
        {
            CloseTheDoor();
            isOpened = true;
        }
    }
}
