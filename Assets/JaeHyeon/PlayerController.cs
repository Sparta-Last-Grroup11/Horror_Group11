using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    [Header("Move")]
    [SerializeField] private float moveSpeed = 5.0f;
    private Vector3 moveInput;
    private Vector3 moveDir;

    [Header("Look")]
    [SerializeField] private Transform cameraContainer;
    [SerializeField] private float lookSensitivity = 0.1f;
    private Vector2 lookInput;
    private float curRotX; // 마우스 위,아래 움직임
    private float curRotY; // 좌,우 움직임
    private float minXLook = -85.0f; // 위,아래 최소값
    private float maxXLook = 85.0f; // 위,아래 최대값

    [Header("Jump")]
    [SerializeField] private float jumpPower = 3.0f;
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

    private void LateUpdate()
    {
        Look();
    }

    private void Move()
    {
        moveDir = transform.forward * moveInput.y + transform.right * moveInput.x + transform.up * moveDir.y;
        if (!characterController.isGrounded)
        {
            moveDir.y += gravity * Time.deltaTime;
        }
        characterController.Move(moveDir * moveSpeed * Time.deltaTime);
    }

    private void Look()
    {
        curRotX -= lookInput.y * lookSensitivity;
        curRotX = Mathf.Clamp(curRotX, minXLook, maxXLook);
        curRotY += lookInput.x * lookSensitivity;
        cameraContainer.localEulerAngles = new Vector3(curRotX, 0, 0);

        transform.eulerAngles = new Vector3(0, curRotY, 0);
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            moveInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            moveInput = Vector2.zero;
        }
    }

    public void LookInput(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && characterController.isGrounded)
        {
            moveDir.y = jumpPower;
        }
    }
}
