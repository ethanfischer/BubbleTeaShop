using UnityEngine;

public class AddingBobaState : IState
{
    private ActiveTea _activeTea;
    const int LEVEL_INDEX = 5;

    public AddingBobaState(ActiveTea activeTea)
    {
        _activeTea = activeTea;
    }

    public void Enter()
    {
        if (Level.Instance.LevelIndex >= LEVEL_INDEX)
        {
            CameraManager.Instance.ActivateBobaPose();
        }
    }

    public void Update()
    {
        OrderSystem.Instance.Tick();

        if (Level.Instance.LevelIndex < LEVEL_INDEX)
        {
            _activeTea.AddRegularBoba();
            StateMachineService.Instance.SetDefaultState();
        }
        else
        {
            if (_activeTea.DidSelectBoba)
            {
                PopupText.Instance.ShowPopup("Boba already selected", 1f);
            }
            else
            {
                BobaSelection();
            }
        }
    }

    void BobaSelection()
    {
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
        if (Level.Instance.LevelIndex >= LEVEL_INDEX)
        {
            CameraManager.Instance.ActivateDefaultPose();
        }
        Debug.Log("Finished Adding Boba.");
    }
}
