using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerState curState;

    public void ChangeState(PlayerState newState)
    {
        curState?.Exit();
        curState = newState;
        curState.Enter();
    }

    public void Update()
    {
        curState?.Update();
    }
}
