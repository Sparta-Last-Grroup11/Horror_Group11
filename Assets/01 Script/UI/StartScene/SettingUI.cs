using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingUI : BaseUI
{
    [SerializeField] Button exitBut;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSlider[] sliders;

    protected override void Start()
    {
        base.Start();

        sliders = GetComponentsInChildren<AudioSlider>();
        foreach (AudioSlider sl in sliders)
        {
            sl.Init(audioMixer);
        }

        exitBut.onClick.AddListener(() =>
        {
            Destroy(gameObject);
        });
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        UIManager.Instance.IsUiActing = false;
    }
}
