public class EnemyStateMachine
{
    private EnemyBaseState _currentState;
    private EnemyBaseState _defaultState;

    public void ChangeState(EnemyBaseState newState)
    {
        _currentState?.Exit();
        _currentState = newState; 
        _currentState?.Enter();
    }

    public void Update()
    {
        _currentState?.Update();
    }

}
