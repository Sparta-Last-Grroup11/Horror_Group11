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
        foreach(var output in playableDirector.playableAsset.outputs)
        {
            if(output.streamName.Equals("Player Track"))
            {
                playableDirector.SetGenericBinding(output.sourceObject, GameManager.Instance.player.GetComponent<Animator>());
            }
        }
        //MonologueManager.Instance.DialogPlay(17);
        //MonologueManager.Instance.DialogPlay(7);
        GameManager.Instance.player.stateMachine.ChangeState(new PlayerIdleState(GameManager.Instance.player));
        QuestManager.Instance.QuestTrigger(questID);
        AudioManager.Instance.Audio2DPlay(seriesSfx);
        GameManager.Instance.player.cantMove = true;
        playableDirector.Play();
    }

    public void MovePlayer()
    {
        GameManager.Instance.player.cantMove = false;
    }

    public void DialogPlay(int num)
    {
        MonologueManager.Instance.DialogPlay(num);
    }

    public void CloseDoubleDoorAndLocked()
    {
        GetComponent<DoubleDoor>().CloseBecauseEnter(changeKey);
    }

    public void WalkingAudioPlay()
    {
        AudioManager.Instance.Audio2DPlay(ResourceManager.Instance.Load<AudioClip>(ResourceType.Sound, "2D/Player/Walk"));
    }
}
