using UnityEngine;

public class AddingCupState : IState
{
    const int LEVEL_INDEX = 4;
    private ActiveTea _activeTea;

    public AddingCupState(ActiveTea activeTea)
    {
        _activeTea = activeTea;
    }

    public void Enter()
    {
        CameraManager.Instance.ActivateCupPose();
        if (Level.Instance.LevelIndex < LEVEL_INDEX)
        {
            CupSelection.Instance.ShowCupSelection(CupSize.Cup);
        }
        else
        {
            CupSelection.Instance.ShowCupSelection(CupSize.LargeCup);
        }
    }

    public void Tick()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StateMachineService.Instance.SetPauseState();
        }
        OrderSystem.Instance.Tick();

        if (Level.Instance.LevelIndex < LEVEL_INDEX)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                _activeTea.AddCup(CupSize.Cup);
                StateMachineService.Instance.SetDefaultState();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                _activeTea.AddCup(CupSize.Cup);
                StateMachineService.Instance.SetDefaultState();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                _activeTea.AddCup(CupSize.LargeCup);
                StateMachineService.Instance.SetDefaultState();
            }
        }
    }

    public void Exit()
    {
        CameraManager.Instance.ActivateDefaultPose();
        CupSelection.Instance.HideCupSelection();
    }
}


public enum CupSize
{
    Cup = 0,
    LargeCup = 1
}
