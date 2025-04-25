using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] EAudioType type;
    Slider slider;
    AudioMixer audioMixer;
    public void Init(AudioMixer mixer)
    {
        audioMixer = mixer;
    }

    private void Start()
    {
        slider = GetComponentInChildren<Slider>();
        AudioManager.Instance.LoadAudioSetting(type, slider);
        slider.onValueChanged.AddListener(SetLevel);
    }

    private void SetLevel(float value)
    {
        float ChangeValue;
        if (value <= 0.001f)
            ChangeValue = -80;
        else
            ChangeValue = Mathf.Log10(value) * 20;
        audioMixer.SetFloat(type.ToString(), ChangeValue);
        AudioManager.Instance.SaveAudioSetting(value, type);
    }
}
