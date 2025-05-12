using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnterEvent : Receiver
{
    [SerializeField] private ItemData changeKey;
    [SerializeField] private int questID = 1;
    [SerializeField] private AudioClip seriesSfx;
    PlayableDirector playableDirector;

    protected override void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    public override void ReceiveTrigger()
    {
        MonologueManager.Instance.DialogPlay(17);
        MonologueManager.Instance.DialogPlay(7);
        QuestManager.Instance.QuestTrigger(questID);
        AudioManager.Instance.Audio2DPlay(seriesSfx);
        GetComponent<DoubleDoor>().CloseBecauseEnter(changeKey);
        GameManager.Instance.player.cantMove = true;
        playableDirector.Play();
        Invoke("MovePlayer", 2f);
    }

    private void MovePlayer()
    {
        GameManager.Instance.player.cantMove = false;
    }
}
