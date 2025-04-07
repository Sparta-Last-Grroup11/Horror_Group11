using UnityEngine;

public abstract class EnemyState
{
    protected Enemy enemy;
    protected EnemyFSM fsm;

    public EnemyState(Enemy enemy, EnemyFSM fsm)
    {
        this.enemy = enemy;
        this.fsm = fsm;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
