using UnityEngine;

public class AddingBobaState : MonoBehaviour, IState
{
    [SerializeField]
    GameObject _strawberryBowl;
    [SerializeField]
    GameObject _blueberryBowl;
    [SerializeField]
    GameObject _mangoBowl;
    [SerializeField]
    GameObject _regularBowl;

    private ActiveTea _activeTea;
    const int LEVEL_INDEX = 5;

    void Start()
    {
        _activeTea = FindObjectOfType<ActiveTea>();
    }

    public void Enter()
    {
        if (Level.Instance.LevelIndex < LEVEL_INDEX)
        {
            _strawberryBowl.SetActive(false);
            _blueberryBowl.SetActive(false);
            _mangoBowl.SetActive(false);
            _regularBowl.SetActive(true);
        }
        else
        {
            _strawberryBowl.SetActive(true);
            _blueberryBowl.SetActive(true);
            _mangoBowl.SetActive(true);
            _regularBowl.SetActive(true);
        }

        CameraManager.Instance.ActivateBobaPose();
    }

    public void Tick()
    {
        OrderSystem.Instance.Tick();

        if (Level.Instance.LevelIndex < LEVEL_INDEX)
        {
            if(Input.GetKeyDown(KeyCode.B))
            {
                _activeTea.AddRegularBoba();
                StateMachineService.Instance.SetDefaultState();
            }
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
        CameraManager.Instance.ActivateDefaultPose();
        Debug.Log("Finished Adding Boba.");
    }
}
