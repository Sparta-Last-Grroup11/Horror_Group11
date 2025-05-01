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
        AudioManager.Instance.Audio3DPlay(flashSwitchClip, transform.position);
        isFlash = !isFlash;
        if (isFlash)
        {
            flashLight.enabled = true;
        }
        else
        {
            flashLight.enabled = false;
        }
    }

    public void OnReloadStarted(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.player.playerInventory.HasItem(battery))
        {
            flash.flashBattery = 100;
            AudioManager.Instance.Audio3DPlay(batterSwitchClip, transform.position);
            GameManager.Instance.player.playerInventory.UseItem(battery);
        }
    }

    private void OnDestroy()
    {
        flashAction.started -= OnFlashStarted;
        reloadAction.started -= OnReloadStarted;
    }
}
