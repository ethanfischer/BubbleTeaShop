using UnityEngine;

public class AddingTeaState : MonoBehaviour, IState
{
    private ActiveTea _activeTea;
    [SerializeField]
    GameObject _taroTeaBowl;
    [SerializeField]
    GameObject _matchaTeaBowl;
    const int LEVEL_INDEX = 6;


    void Start()
    {
        _activeTea = GameObject.FindObjectOfType<ActiveTea>();
    }

    public void Enter()
    {
        CameraManager.Instance.ActivateTeaPose();
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
            if (Input.GetKeyDown(KeyCode.T))
            {
                _activeTea.AddRegularTea();
                StateMachineService.Instance.SetDefaultState();
            }
        }
        else
        {
            if (_activeTea.DidSelectTea)
            {
                PopupText.Instance.ShowPopup("Tea already selected", 1f);
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    _activeTea.AddRegularTea();
                    StateMachineService.Instance.SetDefaultState();
                }
                if (Input.GetKeyDown(KeyCode.R))
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
        }
    }
    
    public void Exit()
    {
        CameraManager.Instance.ActivateDefaultPose();
    }
}
