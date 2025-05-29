using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스타트 씬에서 표기되어야할 UI들을 컨트롤(원래 따로 SceneController 같은 추상 메서드 만들어서 상속받고 이름 통일화 시킬려했는데 잊었네요.)
public class StartUI : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        AudioManager.Instance.AudioBGMPlay(ResourceManager.Instance.Load<AudioClip>(ResourceType.Sound, "BGM/StartSceneBGM"), true, 0.7f, EAudioType.BGM);
        UIManager.Instance.show<StartSceneUI>();
    }
}
