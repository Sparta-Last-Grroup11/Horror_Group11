using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDir;
    private float moveSpeed = 5.0f;
    private float jumpPower = 3f;
    private float gravity = -9.81f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (characterController.isGrounded == false)
        {
            moveDir.y += gravity * Time.deltaTime;
        }

        characterController.Move(moveDir * moveSpeed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputVec;
        if (context.phase == InputActionPhase.Performed)
        {
            inputVec = context.ReadValue<Vector2>();
            moveDir = new Vector3(inputVec.x, moveDir.y, inputVec.y);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            moveDir = Vector2.zero;
        }
    }

    public void MouseDelta(InputAction.CallbackContext context)
    {
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && characterController.isGrounded)
        {
            moveDir = new Vector3(moveDir.x, jumpPower, moveDir.z);
        }
    }
}
