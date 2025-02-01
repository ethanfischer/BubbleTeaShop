using UnityEngine;

public class AddingCupState : IState
{
    private ActiveTea _activeTea;

    public AddingCupState(ActiveTea activeTea)
    {
        _activeTea = activeTea;
    }

    public void Enter()
    {
        CupSelection.Instance.ShowCupSelection();
    }

    public void Update()
    {
        OrderSystem.Instance.Tick();
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

    public void Exit()
    {
        CupSelection.Instance.HideCupSelection();
    }
}


public enum CupSize
{
    Cup = 0,
    LargeCup = 1
}
