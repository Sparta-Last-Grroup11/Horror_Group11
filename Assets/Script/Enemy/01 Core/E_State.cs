using UnityEngine;

public abstract class E_State
{
    protected Enemy enemy;
    protected E_StateMachine fsm;

    public E_State(Enemy enemy, E_StateMachine fsm)
    {
        this.enemy = enemy;
        this.fsm = fsm;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
