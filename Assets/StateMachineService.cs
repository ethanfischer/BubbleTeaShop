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
        SetState(_difficultyMenuState);
    }

    void Update()
    {
        _stateMachine.Update();
    }

    public void SetTutorialState()
    {
        _stateMachine.SetState(_tutorialState);
        _currentStateName = _tutorialState.GetType().ToString();
    }
    
    public void SetState(IState newState)
    {
        Debug.Log("Setting state to " + newState.GetType());
        _stateMachine.SetState(newState);
        _currentStateName = newState.GetType().ToString();
    }
}
