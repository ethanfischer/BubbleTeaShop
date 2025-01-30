public class StateMachine
{
    private IState _currentState;

    public void SetState(IState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void Update()
    {
        _currentState?.Update();
    }
}

public interface IState
{
    void Enter();
    void Update();
    void Exit();
}
