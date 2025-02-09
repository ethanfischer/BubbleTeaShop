using UnityEngine;
using Input = NativeKeyboardHandler;

public class DefaultState : IState
{
    private ActiveTea _activeTea;

    public void Enter()
    {
        _activeTea = GameObject.FindObjectOfType<ActiveTea>();
    }

    public void Tick()
    {
        if(DidPressKey(KeyCode.Escape)) //TODO: figure out on mobile
        {
            StateMachineService.Instance.SetPauseState();
        }
        OrderSystem.Instance.Tick();

        //Cup
        if (!_activeTea.HasCup && DidPressKey(KeyCode.C))
        {
            StateMachineService.Instance.SetAddingCupState(_activeTea);
        }

        if (!_activeTea.HasCup) return;

        //Boba
        if (DidPressKey(KeyCode.B))
        {
            StateMachineService.Instance.SetAddingBobaState();
        }

        //Jelly
        if (DidPressKey(KeyCode.J))
        {
            _activeTea.AddJelly();
        }

        //Ice
        if (DidPressKey(KeyCode.I))
        {
            _activeTea.AddIce();
        }

        //Milk
        if (DidPressKey(KeyCode.M))
        {
            _activeTea.AddMilk();
        }

        //Sugar
        if (DidPressKey(KeyCode.S))
        {
            _activeTea.AddSugar();
        }

        //Tea
        if (DidPressKey(KeyCode.T))
        {
            StateMachineService.Instance.SetAddingTeaState();
        }

        //Toppings
        if (DidPressKey(KeyCode.F))
        {
            _activeTea.AddCheeseFoam();
        }

        //Submit
        if (DidPressKey(KeyCode.Space) || DidPressKey(KeyCode.Return) || DidPressKey(KeyCode.P))
        {
            _activeTea.SubmitTea();
        }
        if (DidPressKey(KeyCode.X))
        {
            _activeTea.TrashTea();
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting DefaultState.");
    }
    
    bool DidPressKey(KeyCode keyCode) => Input.GetKeyDown(keyCode);
}
