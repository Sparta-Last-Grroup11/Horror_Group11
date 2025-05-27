using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EndingSceneTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera endingCutSceneVirtualCamera;
    [SerializeField] private AudioClip endingCutAudioClip;

    private PlayableDirector endingCutSceneDirector;

    private void Awake()
    {
        endingCutSceneDirector = GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.Audio2DPlay(endingCutAudioClip, 1, false, EAudioType.BGM);
            endingCutSceneVirtualCamera.Priority = 14;
            endingCutSceneDirector.Play();
        }
    }
}
