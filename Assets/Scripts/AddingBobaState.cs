using UnityEngine;

public class AddingBobaState : IState
{
    private ActiveTea _activeTea;

    public AddingBobaState(ActiveTea activeTea)
    {
        _activeTea = activeTea;
    }

    public void Enter()
    {
        CameraManager.Instance.ActivateBobaPose();
    }

    public void Update()
    {
        OrderSystem.Instance.Tick();
        if (Input.GetKeyDown(KeyCode.B))
        {
            _activeTea.AddRegularBoba();
            StateMachineService.Instance.SetDefaultState();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            _activeTea.AddMangoBoba();
            StateMachineService.Instance.SetDefaultState();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _activeTea.AddStrawberryBoba();
            StateMachineService.Instance.SetDefaultState();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            _activeTea.AddBlueberryBoba();
            StateMachineService.Instance.SetDefaultState();
        }
    }

    public void Exit()
    {
        CameraManager.Instance.ActivateDefaultPose();
        Debug.Log("Finished Adding Boba.");
    }
}
