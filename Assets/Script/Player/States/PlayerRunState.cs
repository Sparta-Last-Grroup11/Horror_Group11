using UnityEngine;

public class PlayerRunState : PlayerState
{
    private float footTimer = 0f;

    public PlayerRunState(Player player) : base(player) { }

    public override void Enter()
    {

    }

    public override void Update()
    {
        HandleLook();

        footTimer += Time.deltaTime;
        if (footTimer > _player.footSpeedRate / 2)
        {
            AudioManager.Instance.Audio3DPlay(_player.footStep, _player.transform.position);
            footTimer = 0f;
        }

        // 점프 입력 처리
        if (_player.jumpPressed && _player.characterController.isGrounded)
        {
            _player.stateMachine.ChangeState(new PlayerJumpState(_player));
            return;
        }

        // 달리기 이동 처리 (runSpeed 사용)
        Vector3 moveDir = (_player.transform.forward * _player.moveInput.y + _player.transform.right * _player.moveInput.x);
        moveDir *= _player.runSpeed;

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

        // 달리기 상태의 발소리(빠른 주기)와 카메라 흔들림
        if (_player.moveInput.magnitude > 0.2f && _player.characterController.isGrounded)
        {
            _player.StartCameraShake(_player.runShakeIntensity, _player.runShakeFrequency, _player.shakeDuration);
        }

        // 입력 변동에 따른 상태 전환 처리
        if (_player.moveInput.magnitude < 0.2f)
        {
            _player.stateMachine.ChangeState(new PlayerIdleState(_player));
        }
        else if (!_player.runPressing)
        {
            _player.stateMachine.ChangeState(new PlayerWalkState(_player));
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
