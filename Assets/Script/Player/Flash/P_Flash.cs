using UnityEngine;
using UnityEngine.InputSystem;

public class P_Flash : PlayerInputController
{
    public Light flashLight;
    public Flash flash;
    [SerializeField] private AudioClip flashSwitchClip;

    public bool isFlash = false;

    public override void Awake()
    {
        base.Awake();
        flashAction.started += OnFlashStarted;
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
}
