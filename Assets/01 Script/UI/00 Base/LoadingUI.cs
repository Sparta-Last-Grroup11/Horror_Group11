using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : PopupUI
{
    [SerializeField] Image loadingBar;
    //[SerializeField] TextMeshProUGUI loadingText;

    public void SetLoadingProgress(float num)
    {
        loadingBar.fillAmount = num;
        //loadingText.text = (num * 100).ToString();
    }
}
