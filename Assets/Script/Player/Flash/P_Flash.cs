using UnityEngine;
using UnityEngine.InputSystem;

public class P_Flash : MonoBehaviour
{
    public GameObject flashObject;
    public Flash flash;

    public bool isFlash = false;

    public void FlashInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && flash.flashBattery > 0)
        {
            isFlash = !isFlash;
            if (isFlash)
            {
                flashObject.SetActive(true);
            }
            else
            {
                flashObject.SetActive(false);
            }
        }
    }
}
