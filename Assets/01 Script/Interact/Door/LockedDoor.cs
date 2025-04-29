using System.Collections;
using Unity.AI.Navigation;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class LockedDoor : ControlDoor,I_Interactable
{
    [SerializeField] private ItemData key;

    private bool canInteract = true;
    [SerializeField] private float interactCooldown = 1.0f;
    private bool isOpened;

    public void OnInteraction()
    {
        if (!canInteract) return;
        OpenLockedDoor();
        StartCoroutine(InteractionCooldown());
    }

    void OpenLockedDoor()
    {
        if (key != null && !GameManager.Instance.player.playerInventory.HasItem(key))
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
            Open();
        }
        else
        {
            isOpened = false;
            Close();
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
            Open();
            isOpened = true;
        }
    }
}
