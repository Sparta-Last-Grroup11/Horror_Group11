using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputActionMap inputActionMap;

    // Input Action
    protected InputAction moveAction;
    protected InputAction lookAction;
    protected InputAction jumpAction;
    protected InputAction runAction;
    protected InputAction interactAction;
    protected InputAction flashAction;
    protected InputAction reloadAction;
    protected InputAction crouchAction;

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
        reloadAction = inputActionMap.FindAction("Reload");
        crouchAction = inputActionMap.FindAction("Crouch");
        crouchAction = inputActionMap.FindAction("Crouch");
    }
}
