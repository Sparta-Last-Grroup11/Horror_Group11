public class E_StateMachine
{
    private E_BaseState _currentState;

    public void ChangeState(E_BaseState newState)
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
