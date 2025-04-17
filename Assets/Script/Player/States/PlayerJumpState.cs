using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player) : base(player) { }

    public override void Enter()
    {
        if (_player.characterController.isGrounded)
        {
            _player.verticalVelocity = _player.jumpPower;
        }
    }

    public override void Update()
    {
        HandleLook();

        // 점프 중에도 수평 이동(걷기 또는 달리기 입력 적용)
        float currentSpeed = _player.runPressing ? _player.runSpeed : _player.moveSpeed;
        Vector3 moveDir = (_player.transform.forward * _player.moveInput.y + _player.transform.right * _player.moveInput.x) * currentSpeed;

        // 중력 적용
        _player.verticalVelocity += _player.gravity * Time.deltaTime;
        moveDir.y = _player.verticalVelocity;

        _player.characterController.Move(moveDir * Time.deltaTime);

        // 착지하면 상태 전환
        if (_player.characterController.isGrounded)
        {
            _player.jumpPressed = false;
            if (_player.moveInput.magnitude > 0.2f)
            {
                if (_player.runPressing)
                {
                    _player.stateMachine.ChangeState(new PlayerRunState(_player));
                }
                else
                {
                    _player.stateMachine.ChangeState(new PlayerWalkState(_player));
                }
            }
            else
            {
                _player.stateMachine.ChangeState(new PlayerIdleState(_player));
            }
        }
    }

    public override void Exit()
    {

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
