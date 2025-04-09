using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_Flash : MonoBehaviour
{
    public GameObject flash;

    bool isFlash = false;

    public void FlashInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            isFlash = !isFlash;
            if (isFlash)
            {
                flash.SetActive(true);
            }
            else
            {
                flash.SetActive(false);
            }
        }
    }
}
