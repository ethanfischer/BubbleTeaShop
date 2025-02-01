using UnityEngine;

public class AddingTeaState : MonoBehaviour, IState
{
    ActiveTea _activeTea;
    const int LEVEL_INDEX = 6;
    [SerializeField]
    GameObject _taroTeaBowl;
    [SerializeField]
    GameObject _matchaTeaBowl;

    void Start()
    {
        _activeTea = GameObject.FindObjectOfType<ActiveTea>();
    }

    public void Enter()
    {
        CameraManager.Instance.ActivateTeaPose();

        if (Level.Instance.LevelIndex < LEVEL_INDEX)
        {
            _matchaTeaBowl.SetActive(false);
            _taroTeaBowl.SetActive(false);
        }
        else
        {
            _matchaTeaBowl.SetActive(false);
            _taroTeaBowl.SetActive(false);
        }
    }

    public void Update()
    {
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
