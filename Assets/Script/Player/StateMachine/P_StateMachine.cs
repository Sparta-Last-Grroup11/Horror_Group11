using UnityEngine;

public abstract class P_StateMachine : MonoBehaviour
{
    protected Player _player;

    public P_StateMachine (Player player)
    {
        _player = player;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
