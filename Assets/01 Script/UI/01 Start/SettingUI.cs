using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;


public class SettingUI : BaseUI
{
    [SerializeField] Button exitBut;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSlider[] sliders;
    [SerializeField] TMP_Dropdown languageDropdown;

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

        SetupDropDown();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        UIManager.Instance.IsUiActing = false;
    }

    private void SetupDropDown()
    {
        if (languageDropdown == null)
        {
            Debug.Log("DropDown 미할당");
            return;
        }

        languageDropdown.options.Clear();
        int currentIndex = 0;

        string savedLangCode = PlayerPrefs.GetString("SelectedLanguage", null);

        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++)
        {
            var locale = LocalizationSettings.AvailableLocales.Locales[i];
            languageDropdown.options.Add(new TMP_Dropdown.OptionData(locale.LocaleName));

            if (!string.IsNullOrEmpty(savedLangCode) && locale.Identifier.Code == savedLangCode)
            {
                LocalizationSettings.SelectedLocale = locale;
                currentIndex = i;
            }
            else if (LocalizationSettings.SelectedLocale == locale)
            {
                currentIndex = i;
            }
        }
        
        languageDropdown.value = currentIndex;

        languageDropdown.RefreshShownValue();

        languageDropdown.onValueChanged.RemoveAllListeners();
        languageDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void OnDropdownValueChanged(int index)
    {
        var selectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        LocalizationSettings.SelectedLocale = selectedLocale;

        PlayerPrefs.SetString("SelectedLanguage", selectedLocale.Identifier.Code);
        PlayerPrefs.Save();
    }
}
