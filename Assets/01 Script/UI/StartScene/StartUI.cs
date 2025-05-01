using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.AudioBGMPlay(ResourceManager.Instance.Load<AudioClip>(ResourceType.Sound, "StartSceneBGM"), true, 0.7f, EAudioType.BGM);
        UIManager.Instance.show<StartSceneUI>();
    }
}
