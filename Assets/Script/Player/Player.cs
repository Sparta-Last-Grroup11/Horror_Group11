using UnityEngine.InputSystem;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class Player : PlayerInputController
{
    // Components
    private PlayerStateMachine P_StateMachine;
    private CharacterController characterController;
    public CharacterController CharacterController => characterController;

    // footStep
    public AudioClip footStep;
    public float footSpeedRate = 0.2f;

    // ChasedState
    [SerializeField] private AudioClip hardBreath;
    public bool isChased = false;

    // Input Value
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
    

    public override void Awake()
    {
        base.Awake();
        characterController = GetComponent<CharacterController>();

        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCanceled;
        lookAction.performed += OnLookPerformed;
        lookAction.canceled += OnLookCanceled;
        jumpAction.started += OnJumpStarted;
        jumpAction.canceled += OnJumpCanceled;
        runAction.performed += OnRunPerformed;
        runAction.canceled += OnRunCanceled;

        P_StateMachine = new PlayerStateMachine();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        P_StateMachine.ChangeState(new PlayerMoveState(this));

        if (virtualCamera != null)
        {
            camNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    private void Update()
    {
        P_StateMachine.Update();
        if (isChased)
        {
            AudioManager.Instance.Audio3DPlay(hardBreath, transform.position);
            isChased = false;
        }
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private void OnLookCanceled(InputAction.CallbackContext context)
    {
        lookInput = Vector2.zero;
    }

    private void OnJumpStarted(InputAction.CallbackContext context)
    {
        jumpPressed = true;
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        jumpPressed = false;
    }

    private void OnRunPerformed(InputAction.CallbackContext context)
    {
        runPressing = true;
    }

    private void OnRunCanceled(InputAction.CallbackContext context)
    {
        runPressing = false;
    }
}