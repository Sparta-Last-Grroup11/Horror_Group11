using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : ControlDoor,I_Interactable
{
    [SerializeField] private EItemID keyID;
    [SerializeField] private GetItemList itemList;

    private void Start()
    {
        OnInteraction();
    }
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
