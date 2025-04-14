using UnityEngine;
using UnityEngine.InputSystem;

public class P_Flash : PlayerInputController
{
    public Light flashLight;
    public Flash flash;

    public bool isFlash = false;

    public override void Awake()
    {
        base.Awake();
        flashAction.started += OnFlashStarted;
    }

    public void OnFlashStarted(InputAction.CallbackContext context)
    {
        Debug.Log("눌림");
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
