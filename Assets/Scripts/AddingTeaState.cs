public class AddingTeaState : IState
{
    private ActiveTea _activeTea;
    private StateMachine _stateMachine;

    public AddingTeaState(ActiveTea activeTea, StateMachine stateMachine)
    {
        _activeTea = activeTea;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
    }

    public void Update() { }  // No update logic needed
    public void Exit() { }
}
