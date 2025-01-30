using UnityEngine;

public class AddingBobaState : IState
{
    private ActiveTea _activeTea;
    private StateMachine _stateMachine;

    public AddingBobaState(ActiveTea activeTea, StateMachine stateMachine)
    {
        _activeTea = activeTea;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        CameraManager.Instance.ActivateBobaPose();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _activeTea.AddRegularBoba();
            _stateMachine.SetState(new DefaultState(_activeTea, _stateMachine));
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            _activeTea.AddMangoBoba();
            _stateMachine.SetState(new DefaultState(_activeTea, _stateMachine));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _activeTea.AddStrawberryBoba();
            _stateMachine.SetState(new DefaultState(_activeTea, _stateMachine));
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            _activeTea.AddBlueberryBoba();
            _stateMachine.SetState(new DefaultState(_activeTea, _stateMachine));
        }
    }

    public void Exit()
    {
        CameraManager.Instance.ActivateDefaultPose();
        Debug.Log("Finished Adding Boba.");
    }
}
