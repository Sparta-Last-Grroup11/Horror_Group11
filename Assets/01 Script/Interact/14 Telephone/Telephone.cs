using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Telephone : MonoBehaviour, I_Interactable
{
    [SerializeField] private CinemachineVirtualCamera telephoneVirtualCamera;
    [SerializeField] private Image fadeOutUI;
    [SerializeField] private AudioClip telephoneDialingClip;
    [SerializeField] private AudioClip telephoneTalkClip;
    [SerializeField] private AudioClip sirenClip;

    private bool isPowerOn = false;

    public void OnInteraction()
    {
        if (isPowerOn == false) return;
        if (GameManager.Instance.player.isChased)
        {
            MonologueManager.Instance.DialogPlay(28);
            return;
        }
        telephoneVirtualCamera.Priority = 12;
        GameManager.Instance.player.is911Calling = true;
        StartCoroutine(TelephoneSound());
    }

    IEnumerator TelephoneSound()
    {
        AudioManager.Instance.Audio2DPlay(telephoneDialingClip, 1, false, EAudioType.SFX);
        yield return new WaitForSeconds(telephoneDialingClip.length);
        AudioManager.Instance.Audio2DPlay(telephoneTalkClip, 1, false, EAudioType.SFX);
        yield return new WaitForSeconds(3f);
        AudioManager.Instance.Audio2DPlay(sirenClip, 1, false, EAudioType.SFX);
        yield return new WaitForSeconds(1f);
        fadeOutUI.DOFade(1, 3);
        yield return new WaitForSeconds(3f);
        UIManager.Instance.show<EndGameUI>().ShowEnding(EndingCategory.Rescued, 1000);
    }

    public void OnPower()
    {
        isPowerOn = true;
    }
}
