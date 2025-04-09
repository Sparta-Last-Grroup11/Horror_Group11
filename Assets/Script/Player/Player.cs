using UnityEngine.InputSystem;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    private PlayerState curState;
    private CharacterController characterController;

    public CharacterController CharacterController => characterController;

    public Vector2 moveInput;
    public Vector2 lookInput;
    public bool jumpPressed;
    public bool runPressing;

    [Header("Move")]
    public float moveSpeed = 2f;
    public float runSpeed = 2f;

    [Header("Look")]
    public Transform cameraContainer;
    public float lookSensitivity = 0.1f;
    public float minXLook = -85.0f;
    public float maxXLook = 85.0f;
    public float curRotX;
    public float curRotY;

    [Header("Jump")]
    public float jumpPower = 2f;
    public float gravity = -9.81f;
    public float verticalVelocity;

    [Header("Camera Shake")]
    public CinemachineVirtualCamera virtualCamera;
    public float walkShakeIntensity = 0.8f;
    public float runShakeIntensity = 1.6f;
    public float walkShakeFrequency = 0.02f;
    public float runShakeFrequency = 0.05f;
    public float shakeDuration = 0.1f;

    [HideInInspector] public float shakeTimer = 0f;
    [HideInInspector] public CinemachineBasicMultiChannelPerlin camNoise;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        ChangeState(new PlayerMoveState(this));

        if (virtualCamera != null)
        {
            camNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    private void Update()
    {
        curState?.Update();
    }

    public void ChangeState(PlayerState newState)
    {
        curState?.Exit();
        curState = newState;
        curState.Enter();
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            moveInput = context.ReadValue<Vector2>();
        else if (context.phase == InputActionPhase.Canceled)
            moveInput = Vector2.zero;
    }

    public void LookInput(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            jumpPressed = true;
    }

    public void RunInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            runPressing = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            runPressing = false;
        }
    }
}
