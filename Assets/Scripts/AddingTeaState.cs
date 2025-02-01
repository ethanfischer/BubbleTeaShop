using UnityEngine;

public class AddingTeaState : IState
{
    private ActiveTea _activeTea;

    public AddingTeaState(ActiveTea activeTea)
    {
        _activeTea = activeTea;
    }

    public void Enter()
    {
        CameraManager.Instance.ActivateTeaPose();
    }

    public void Update()
    {
        OrderSystem.Instance.Tick();
        if (Input.GetKeyDown(KeyCode.R))
        {
            _activeTea.AddRegularTea();
            StateMachineService.Instance.SetDefaultState();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            _activeTea.AddTaroTea();
            StateMachineService.Instance.SetDefaultState();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            _activeTea.AddMatchaTea();
            StateMachineService.Instance.SetDefaultState();
        }
    }
    public void Exit()
    {
        CameraManager.Instance.ActivateDefaultPose();
    }
}
