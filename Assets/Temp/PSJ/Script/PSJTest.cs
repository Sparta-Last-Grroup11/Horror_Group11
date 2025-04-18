using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSJTest : MonoBehaviour
{
    [SerializeField] List<AudioClip> bgm;

    [SerializeField] AudioClip clip;

    private void Start()
    {
        Invoke(nameof(HeartBeatStart), 2f);
        Invoke(nameof(HeartBeatStop), 5f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            AudioManager.Instance.AudioBGMPlay(bgm[0]);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            AudioManager.Instance.AudioBGMPlay(bgm[1]);
        else if (Input.GetKeyDown(KeyCode.KeypadEnter))
            AudioManager.Instance.Audio3DPlay(clip, transform.position);
    }

    private void HeartBeatStart()
    {
        UIManager.Instance.Get<HeartBeat>().ChanbeatSpeed(1);
    }

    private void HeartBeatStop()
    {
        UIManager.Instance.Get<HeartBeat>().ChanbeatSpeed(0);
    }

    void DyingUI()
    {
        UIManager.Instance.show<DyingUI>();
    }
}
