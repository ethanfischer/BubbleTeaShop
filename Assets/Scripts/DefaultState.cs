using UnityEngine;

public class DefaultState : IState
{
    private ActiveTea _activeTea;
    private StateMachine _stateMachine;

    public DefaultState(ActiveTea activeTea, StateMachine stateMachine)
    {
        _activeTea = activeTea;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        Debug.Log("Entered IdleState.");
    }

    public void Update()
    {
        //Boba
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (Level.Instance.LevelIndex < 4)
            {
                _activeTea.AddRegularBoba();
            }
            else
            {
                if (_activeTea.DidSelectBoba)
                {
                    PopupText.Instance.ShowPopup("Boba already selected", 1f);
                }
                else
                {
                    _stateMachine.SetState(new AddingBobaState(_activeTea, _stateMachine)); //TODO: refactor so all these conditions live in the boba state
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            _activeTea.AddJelly();
        }

        //Ice
        if (Input.GetKeyDown(KeyCode.I))
        {
            _activeTea.AddIceScoop();
        }

        //Milk
        if (Input.GetKeyDown(KeyCode.M))
        {
            _activeTea.AddMilk();
        }

        //Sugar
        if (Input.GetKeyDown(KeyCode.S))
        {
            _activeTea.AddSugar();
        }

        //Tea
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (Level.Instance.LevelIndex < 5)
            {
                _activeTea.AddRegularTea();
            }
            else
            {
                if (_activeTea.DidSelectTea)
                {
                    PopupText.Instance.ShowPopup("Tea already selected", 1f);
                }
                else
                {
                    _stateMachine.SetState(new AddingTeaState(_activeTea, _stateMachine)); //TODO: refactor so all these conditions live in the boba state
                }
            }
        }

        //Toppings
        if (Input.GetKeyDown(KeyCode.C))
        {
            _activeTea.AddCheeseFoam();
        }

        //Submit
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            _activeTea.SubmitTea();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            _activeTea.TrashTea();
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting IdleState.");
    }
}
