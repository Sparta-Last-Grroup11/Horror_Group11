using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PowerOn : MonoBehaviour, I_Interactable
{
    [SerializeField] private bool isAct;
    [SerializeField] private SwitchController elecBox;
    [SerializeField] private AudioClip clip;
    [SerializeField] private int questID = 4;
    private PlayableDirector playableDirector;

    private void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    public void OnInteraction()
    {
        if (isAct) return;
        isAct = true;
        if(clip != null)
            AudioManager.Instance.Audio2DPlay(clip);
        elecBox.OnPower();
        MonologueManager.Instance.DialogPlay(10);
        MonologueManager.Instance.DialogPlay(9);
        QuestManager.Instance.QuestTrigger(questID);
        playableDirector.Play();
        GameManager.Instance.player.cantMove = true;
        Invoke("PlayerCanMove", 3f);
    }

    private void PlayerCanMove()
    {
        GameManager.Instance.player.cantMove = false;
    }
}
