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
    public bool IsOpened => isOpened;
    [SerializeField] private AudioClip lockedSound;
    private bool wasOpend = false;

    [SerializeField] private int questID = -1;
    public void OnInteraction()
    {
        if (!canInteract) return;
        OpenLockedDoor();
        StartCoroutine(InteractionCooldown());
    }

    void OpenLockedDoor()
    {
        QuestManager.Instance.QuestTrigger(questID);
        if (key != null && !GameManager.Instance.player.playerInventory.HasItem(key))
        {
            MonologueManager.Instance.DialogPlay(16);
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
            wasOpend = true;
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
        if (!isOpened && (key == null || wasOpend))
        {
            Open();
            isOpened = true;
        }
    }
}
