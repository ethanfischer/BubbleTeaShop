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
        if (Input.GetKeyDown(KeyCode.R))
        {
            _activeTea.AddRegularBoba();
            StateMachineService.Instance.SetState(new DefaultState());
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            _activeTea.AddMangoBoba();
            StateMachineService.Instance.SetState(new DefaultState());
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _activeTea.AddStrawberryBoba();
            StateMachineService.Instance.SetState(new DefaultState());
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            _activeTea.AddBlueberryBoba();
            StateMachineService.Instance.SetState(new DefaultState());
        }
    }

    public void Exit()
    {
        CameraManager.Instance.ActivateDefaultPose();
        Debug.Log("Finished Adding Boba.");
    }
}
