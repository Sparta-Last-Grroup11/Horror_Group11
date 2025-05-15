using UnityEngine;

public class EnemyBaseState
{
    protected Enemy enemy;
    protected EnemyStateMachine fsm;

    public EnemyBaseState(Enemy enemy, EnemyStateMachine fsm)
    {
        this.enemy = enemy;
        this.fsm = fsm;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }

}
