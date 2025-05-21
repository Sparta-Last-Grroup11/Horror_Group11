using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RamainFlashBatteryUI : BaseUI
{
    [SerializeField] Image remainFlashBattery;

    private void Update()
    {
        remainFlashBattery.fillAmount = GameManager.Instance.player.flash.flashBattery / 120f;
    }
}
