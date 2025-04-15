using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputActionMap inputActionMap;

    // Input Action
    public InputAction moveAction;
    public InputAction lookAction;
    public InputAction jumpAction;
    public InputAction runAction;
    public InputAction interactAction;
    public InputAction flashAction;

    public virtual void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        inputActionMap = playerInput.actions.FindActionMap("Player");

        moveAction = inputActionMap.FindAction("Movement");
        lookAction = inputActionMap.FindAction("Look");
        jumpAction = inputActionMap.FindAction("Jump");
        runAction = inputActionMap.FindAction("Run");
        interactAction = inputActionMap.FindAction("Interact");
        flashAction = inputActionMap.FindAction("Flash");
    }
}
