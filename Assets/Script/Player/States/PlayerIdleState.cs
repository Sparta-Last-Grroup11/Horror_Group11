using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player) : base(player) { }

    public override void Enter()
    {

    }

    public override void Update()
    {
        HandleLook();

        // 이동 입력이 있을 경우 Walk 또는 Run 상태로 전환
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
            return;
        }

        // 점프 입력이 있을 경우 Jump 상태로 전환
        if (_player.jumpPressed && _player.characterController.isGrounded)
        {
            _player.stateMachine.ChangeState(new PlayerJumpState(_player));
            return;
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
