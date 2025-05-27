using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnterEvent : Receiver //정문 입장 시 이벤트용 리시버
{
    [SerializeField] private ItemData changeKey;
    [SerializeField] private int questID = 1;
    PlayableDirector playableDirector;
    CinemachineFreeLook freeLook;

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
        GameManager.Instance.player.cantMove = true;
        GameManager.Instance.player.cameraContainer.transform.localRotation = Quaternion.Euler(0, 0, 0);
        playableDirector.Play();
        UIManager.Instance.show<SkipUI>().Init(1, SkipEvent);
    }

    public void MovePlayer()
    {
        Destroy(UIManager.Instance.Get<SkipUI>().gameObject);
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

    private void SkipEvent()
    {
        playableDirector.Stop();
        GameManager.Instance.player.transform.position = new Vector3(-6f, 1.75f, 0);
        GameManager.Instance.player.transform.rotation = Quaternion.Euler(0, -90f, 0);
        CloseDoubleDoorAndLocked();
        MonologueManager.Instance.DialogPlay(7);
        GameManager.Instance.player.cantMove = false;
    }
}
