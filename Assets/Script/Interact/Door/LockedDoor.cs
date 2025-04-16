using System.Collections;
using Unity.AI.Navigation;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class LockedDoor : ControlDoor,I_Interactable
{
    [SerializeField] private EItemID keyID;
    [SerializeField] private GetItemList itemList;

    private bool canInteract = true;
    [SerializeField] private float interactCooldown = 1.0f;
    private bool isOpened;

    [Header("NavMeshSetting")]
    public NavMeshModifierVolume navMeshVolume;
    public int closedArea = 1; // Not Walkable
    public int openArea = 3;   // PassThroughDoor

    public void OnInteraction()
    {
        if (!canInteract) return;
        OpenLockedDoor();
        StartCoroutine(InteractionCooldown());
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
        if (!isOpened)
        {
            isOpened = true;
            OpenTheDoor();
        }
        else
        {
            isOpened = false;
            CloseTheDoor();
        }
    }

    private IEnumerator InteractionCooldown()
    {
        canInteract = false;
        yield return new WaitForSeconds(interactCooldown);
        canInteract = true;
    }
    
    public void MonstersOpen()
    {
        if (!isOpened)
        {
            OpenTheDoor();
            isOpened = true;
        }
    }
}
