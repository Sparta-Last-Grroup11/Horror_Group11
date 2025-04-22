using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoor : MonoBehaviour,I_Interactable
{
    [SerializeField] private ItemData key;
    [SerializeField] private ControlDoor[] doors;

    private bool canInteract = true;
    [SerializeField] private float interactCooldown = 1.0f;
    protected bool isOpened;

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
            foreach(var door in doors)
                door.Open();
        }
        else
        {
            isOpened = false;
            foreach (var door in doors)
                door.Close();
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
            foreach (var door in doors)
                door.Open();
            isOpened = true;
        }
    }

}
