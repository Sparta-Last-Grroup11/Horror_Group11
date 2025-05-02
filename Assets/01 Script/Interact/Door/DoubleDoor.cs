using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoor : MonoBehaviour,I_Interactable
{
    [SerializeField] private ItemData key;
    [SerializeField] private ControlDoor[] doors;

    private bool canInteract = true;
    [SerializeField] private float interactCooldown = 1.0f;
    [SerializeField] protected bool isOpened;
    [SerializeField] private AudioClip lockedSound;

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
            MonologueManager.Instance.DialogPlay("This door is locked.");
            if (lockedSound != null)
            {
                AudioManager.Instance.Audio3DPlay(lockedSound, transform.position);
            }
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

    public void CloseBecauseEnter(ItemData changeKey)
    {
        if (isOpened)
        {
            foreach (var door in doors)
            {
                door.Close();
            }
            isOpened = false;
        }
        key = changeKey;
    }
}
