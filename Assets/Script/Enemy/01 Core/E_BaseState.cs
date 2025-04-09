public abstract class E_BaseState : I_State
{
    protected Enemy enemy;
    protected E_StateMachine fsm;

    public E_BaseState(Enemy enemy, E_StateMachine fsm)
    {
        this.enemy = enemy;
        this.fsm = fsm;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { } 
}
