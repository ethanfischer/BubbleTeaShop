using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineService : MonoBehaviour
{
    [SerializeField]
    TutorialState _tutorialState;
    [SerializeField]
    DifficultyMenuState _difficultyMenuState;
    
    [SerializeField]
    string _currentStateName;
    
    //unity singleton pattern
    private static StateMachineService _instance;
    public static StateMachineService Instance
    { get
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<StateMachineService>();
        }
        return _instance;
    } }

    StateMachine _stateMachine;

    void Start()
    {
        _stateMachine = new StateMachine();
        SetDifficultyMenuState();
    }

    void Update()
    {
        _stateMachine.Update();
    }
    
    void SetDifficultyMenuState()
    {
        _stateMachine.SetState(_difficultyMenuState);
        _currentStateName = _difficultyMenuState.GetType().ToString();
    }

    public void SetTutorialState()
    {
        _stateMachine.SetState(_tutorialState);
        _currentStateName = _tutorialState.GetType().ToString();
    }
    
    public void SetDefaultState()
    {
        var defaultState = new DefaultState();
        _stateMachine.SetState(defaultState);
        _currentStateName = defaultState.GetType().ToString();
    }
    
    public void SetAddingTeaState(ActiveTea activeTea)
    {
        var addingTeaState = new AddingTeaState(activeTea);
        _stateMachine.SetState(addingTeaState);
        _currentStateName = addingTeaState.GetType().ToString();
    }
    
    public void SetAddingBobaState(ActiveTea activeTea)
    {
        var addingBobaState = new AddingBobaState(activeTea);
        _stateMachine.SetState(addingBobaState);
        _currentStateName = addingBobaState.GetType().ToString();
    }
    
    // public void SetState(IState newState)
    // {
    //     Debug.Log("Setting state to " + newState.GetType());
    //     _stateMachine.SetState(newState);
    //     _currentStateName = newState.GetType().ToString();
    // }
}
