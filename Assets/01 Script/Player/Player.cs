using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using DG.Tweening;

public class Player : PlayerInputController
{
    public PlayerStateMachine stateMachine;
    public CharacterController characterController;
    public PlayerInventory playerInventory;

    // 발소리 관련
    public AudioClip footStepClip;
    public AudioClip runStepClip;
    public AudioClip jumpStartClip;
    public AudioClip jumpLandClip;
    public float footSpeedRate = 0.2f;

    // 쫓기는 상태 관련
    [SerializeField] private AudioClip chasedCilp;
    public bool isChased = false;
    private bool isChasedBGM = false;
    public bool cantMove = false;

    // 입력 값
    public Vector2 moveInput;
    public Vector2 lookInput;
    public bool jumpPressed;
    public bool runPressing;

    [Header("Move")]
    public float moveSpeed = 2f;
    public float runSpeed = 4f;
    public bool isCrouching;

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
        stateMachine = new PlayerStateMachine();
        playerInventory = new PlayerInventory();

        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCanceled;
        lookAction.performed += OnLookPerformed;
        lookAction.canceled += OnLookCanceled;
        jumpAction.started += OnJumpStarted;
        jumpAction.canceled += OnJumpCanceled;
        runAction.performed += OnRunPerformed;
        runAction.canceled += OnRunCanceled;
        crouchAction.performed += OnCrouchPerformed;
        crouchAction.canceled += OnCrouchCanceled;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // 초기 상태를 Idle로 설정
        stateMachine.ChangeState(new PlayerIdleState(this));
        cantMove = false;

        if (virtualCamera != null)
        {
            camNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    private void Update()
    {
        InventoryOpen();
        if (isChased)
        {
            ChasingByEnemy();
        }
        else
        {
            isChasedBGM = false;
        }

        if (!UIManager.Instance.IsUiActing && !cantMove)
        {
            stateMachine.Update();
        }
    }

    // 입력 콜백들
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
        if (!isCrouching)
        {
            runPressing = true;
        }
    }

    private void OnRunCanceled(InputAction.CallbackContext context)
    {
        runPressing = false;
    }

    private void OnCrouchPerformed(InputAction.CallbackContext context)
    {
        transform.DOScaleY(0.6f, 0.2f);
        moveSpeed--;
        isCrouching = true;
    }

    private void OnCrouchCanceled(InputAction.CallbackContext context)
    {
        transform.DOScaleY(1f, 0.2f);
        moveSpeed++;
        isCrouching = false;
    }

    // 카메라 흔들림 관련 메서드
    public void StartCameraShake(float intensity, float frequency, float duration)
    {
        if (camNoise != null)
        {
            camNoise.m_AmplitudeGain = intensity;
            camNoise.m_FrequencyGain = frequency;
            shakeTimer = duration;
        }
    }

    public void StopCameraShake()
    {
        // 흔들림을 멈추는 로직, 예: 진폭이나 진동수 0으로 설정
        camNoise.m_AmplitudeGain = 0f;
        camNoise.m_FrequencyGain = 0f;
    }

    private void ChasingByEnemy()
    {
        if (!isChasedBGM)
        {
            AudioManager.Instance.Audio2DPlay(chasedCilp, 1f);
            isChasedBGM = true;
        }
    }

    //인벤토리 열기
    private void InventoryOpen()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
            playerInventory.ShowInventory();
    }
}
