using UnityEngine;

public class PlayerWalkState : PlayerState
{
    private float footTimer = 0f;

    public PlayerWalkState(Player player) : base(player) { }

    public override void Enter()
    {

    }

    public override void Update()
    {
        HandleLook();

        footTimer += Time.deltaTime;
        if (footTimer > _player.footSpeedRate)
        {
            AudioManager.Instance.Audio3DPlay(_player.footStepClip, _player.transform.position);
            footTimer = 0f;
        }

        // 점프 입력이 있으면 Jump 상태로 전환
        if (_player.jumpPressed && _player.characterController.isGrounded)
        {
            _player.stateMachine.ChangeState(new PlayerJumpState(_player));
            return;
        }

        // 걷기 이동 처리 (moveSpeed 사용)
        Vector3 moveDir = (_player.transform.forward * _player.moveInput.y + _player.transform.right * _player.moveInput.x);
        moveDir *= _player.moveSpeed;

        // 중력 처리
        if (_player.characterController.isGrounded)
        {
            _player.verticalVelocity = -1f;
        }
        else
        {
            _player.verticalVelocity += _player.gravity * Time.deltaTime;
        }
        moveDir.y = _player.verticalVelocity;

        _player.characterController.Move(moveDir * Time.deltaTime);

        // 이동 중 발소리와 카메라 흔들림 처리
        if (_player.moveInput.magnitude > 0.2f && _player.characterController.isGrounded)
        {
            _player.StartCameraShake(_player.walkShakeIntensity, _player.walkShakeFrequency, _player.shakeDuration);
        }

        // 입력이 없으면 Idle, 달리기 입력이 있으면 Run 상태로 전환
        if (_player.moveInput.magnitude < 0.2f)
        {
            _player.stateMachine.ChangeState(new PlayerIdleState(_player));
        }
        else if (_player.runPressing)
        {
            _player.stateMachine.ChangeState(new PlayerRunState(_player));
        }
    }

    public override void Exit()
    {
        _player.StopCameraShake();
    }

    private void HandleLook()
    {
        _player.curRotX -= _player.lookInput.y * _player.lookSensitivity;
        _player.curRotX = Mathf.Clamp(_player.curRotX, _player.minXLook, _player.maxXLook);
        _player.curRotY += _player.lookInput.x * _player.lookSensitivity;

        _player.cameraContainer.localEulerAngles = new Vector3(_player.curRotX, 0, 0);
        _player.transform.eulerAngles = new Vector3(0, _player.curRotY, 0);
    }
}
