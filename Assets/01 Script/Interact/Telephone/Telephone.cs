using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telephone : MonoBehaviour, I_Interactable
{
    [SerializeField] private CinemachineVirtualCamera telephoneVirtualCamera;
    [SerializeField] private AudioClip telephoneDialingClip;
    [SerializeField] private AudioClip telephoneTalkClip;
    [SerializeField] private AudioClip sirenClip;

    public void OnInteraction()
    {
        telephoneVirtualCamera.Priority = 12;
        StartCoroutine(TelephoneSound());
    }

    IEnumerator TelephoneSound()
    {
        AudioManager.Instance.Audio2DPlay(telephoneDialingClip);
        yield return new WaitForSeconds(telephoneDialingClip.length);
        AudioManager.Instance.Audio2DPlay(telephoneTalkClip);
        yield return new WaitForSeconds(3f);
        AudioManager.Instance.Audio2DPlay(sirenClip);
        yield return new WaitForSeconds(2f);
        UIManager.Instance.show<TelephoneUI>();
    }
}
