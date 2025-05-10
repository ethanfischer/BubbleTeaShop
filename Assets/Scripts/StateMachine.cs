public class StateMachine
{
    public IState CurrentState { get; private set; }

    public void SetState(IState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public void Update()
    {
        CurrentState?.Tick();
    }
}

public interface IState
{
    void Enter();
    void Tick();
    void Exit();
}
