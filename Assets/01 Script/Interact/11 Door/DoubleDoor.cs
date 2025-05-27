using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoor : MonoBehaviour,I_Interactable //양 문
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

    void OpenLockedDoor() //문 여닫기 조건 확인
    {
        if (key != null && !GameManager.Instance.player.playerInventory.HasItem(key)) //열쇠가 없을 경우
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

     void OpenCloseDoor() //문 여닫기
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

    private IEnumerator InteractionCooldown() //상호작용 쿨타임
    {
        canInteract = false;
        yield return new WaitForSeconds(interactCooldown);
        canInteract = true;
    }

    public void CloseBecauseEnter(ItemData changeKey) //트리거를 통해 문 조작 조건(열쇠) 변경
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
