using UnityEngine;

public class AddingCupState : IState
{
    const int LEVEL_INDEX = 3;
    private ActiveTea _activeTea;

    public AddingCupState(ActiveTea activeTea)
    {
        _activeTea = activeTea;
    }

    public void Enter()
    {
        if (Level.Instance.LevelIndex >= LEVEL_INDEX)
        {
            CupSelection.Instance.ShowCupSelection();
        }
    }

    public void Update()
    {
        OrderSystem.Instance.Tick();

        if (Level.Instance.LevelIndex < LEVEL_INDEX)
        {
            _activeTea.AddCup(CupSize.Cup);
            StateMachineService.Instance.SetDefaultState();
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
        if (Level.Instance.LevelIndex >= LEVEL_INDEX)
        {
            CupSelection.Instance.HideCupSelection();
        }
    }
}


public enum CupSize
{
    Cup = 0,
    LargeCup = 1
}
