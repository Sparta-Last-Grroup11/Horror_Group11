using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private float afterLastFootStep;

    public PlayerMoveState(Player player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Movement 상태 시작");
    }

    public override void Update()
    {
        if (UIManager.Instance.IsUiActing) return;
        HandleLook();
        HandleMove();
        HandleCameraShake();
    }

    public override void Exit()
    {
        Debug.Log("Movement 상태 종료");
    }

    private void HandleLook()
    {
        _player.curRotX -= _player.lookInput.y * _player.lookSensitivity;
        _player.curRotX = Mathf.Clamp(_player.curRotX, _player.minXLook, _player.maxXLook);
        _player.curRotY += _player.lookInput.x * _player.lookSensitivity;

        _player.cameraContainer.localEulerAngles = new Vector3(_player.curRotX, 0, 0);
        _player.transform.eulerAngles = new Vector3(0, _player.curRotY, 0);
    }

    private void HandleMove()
    {
        Vector3 moveDir =
            (_player.transform.forward * _player.moveInput.y +
            _player.transform.right * _player.moveInput.x) * (_player.runPressing ? _player.runSpeed : 1f);

        if (_player.CharacterController.isGrounded)
        {
            _player.verticalVelocity = _player.jumpPressed ? _player.jumpPower : moveDir.y;
            _player.jumpPressed = false;
        }
        else
        {
            _player.verticalVelocity += _player.gravity * Time.deltaTime;
        }

        moveDir.y = _player.verticalVelocity;
        _player.CharacterController.Move(moveDir * _player.moveSpeed * Time.deltaTime);

        bool isMoving = _player.moveInput.magnitude > 0.2f;
        if (isMoving && _player.CharacterController.isGrounded)
        {
            afterLastFootStep += Time.deltaTime;
            if (afterLastFootStep > (_player.footSpeedRate = _player.runPressing ? 0.16f : 0.5f))
            {
                AudioManager.Instance.Audio3DPlay(_player.footStep, _player.transform.position);
                afterLastFootStep = 0f;
            }
            float intensity = _player.runPressing ? _player.runShakeIntensity : _player.walkShakeIntensity;
            float frequency = _player.runPressing ? _player.runShakeFrequency : _player.walkShakeFrequency;

            StartCameraShake(intensity, frequency, _player.shakeDuration);
        }
    }

    private void HandleCameraShake()
    {
        if (_player.shakeTimer > 0)
        {
            _player.shakeTimer -= Time.deltaTime;
            if (_player.shakeTimer <= 0 && _player.camNoise != null)
            {
                _player.camNoise.m_AmplitudeGain = 0f;
                _player.camNoise.m_FrequencyGain = 0f;
            }
        }
    }

    private void StartCameraShake(float intensity, float frequency, float duration)
    {
        if (_player.camNoise != null)
        {
            _player.camNoise.m_AmplitudeGain = intensity;
            _player.camNoise.m_FrequencyGain = frequency;
            _player.shakeTimer = duration;
        }
    }
}
