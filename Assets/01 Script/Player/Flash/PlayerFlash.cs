using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFlash : PlayerInputController
{
    public Light flashLight;
    public Flash flash;
    [SerializeField] private AudioClip flashSwitchClip;
    [SerializeField] private AudioClip batterSwitchClip;
    [SerializeField] private ItemData battery;

    public bool isFlash = false;

    public override void Awake()
    {
        base.Awake();
        flashAction.started += OnFlashStarted;
        reloadAction.started += OnReloadStarted;
    }

    public void OnFlashStarted(InputAction.CallbackContext context)
    {
        //1. 인풋시스템 문제
        if (GameManager.Instance.player.isDead == false && UIManager.Instance.IsUiActing == false)
        {
            AudioManager.Instance.Audio3DPlay(flashSwitchClip, transform.position);
            isFlash = !isFlash;
            if (isFlash && flash.flashBattery > 0)
            {
                flashLight.enabled = true;
            }
            else
            {
                flashLight.enabled = false;
            }
        }
    }

    public void OnReloadStarted(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.player.isDead == false && UIManager.Instance.IsUiActing == false)
        {
            if (GameManager.Instance.player.playerInventory.HasItem(battery))
            {
                flash.flashBattery = 120;
                AudioManager.Instance.Audio3DPlay(batterSwitchClip, transform.position);
                GameManager.Instance.player.playerInventory.UseItem(battery);
            }
        }
    }

    private void OnDestroy()
    {
        flashAction.started -= OnFlashStarted;
        reloadAction.started -= OnReloadStarted;
    }
}
