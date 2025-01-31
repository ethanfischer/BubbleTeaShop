public class AddingTeaState : IState
{
    private ActiveTea _activeTea;

    public AddingTeaState(ActiveTea activeTea)
    {
        _activeTea = activeTea;
    }

    public void Enter()
    {
    }

    public void Update() { }  // No update logic needed
    public void Exit() { }
}
