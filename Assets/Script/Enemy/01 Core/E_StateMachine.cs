public class E_StateMachine
{
    private I_State _currentState;

    public void ChangeState(I_State newState)
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
