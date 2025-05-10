using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyMenuState : MonoBehaviour, IState
{
    [SerializeField]
    Animator _animator;
    [SerializeField]
    CanvasGroup _canvasGroup;
    [SerializeField]
    Image _image;
    [SerializeField]
    CanvasGroup _textGroup;
    [SerializeField]
    Color _fadeColor = Color.black;

    public void Enter()
    {
        _canvasGroup.alpha = 1f; //Show
    }
    
    void IState.Tick()
    {
        _canvasGroup.alpha = 1f;
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameDifficulty.Difficulty = (int)GameDifficultyEnum.Easy;
            StateMachineService.Instance.SetDefaultState();
            Level.Instance.NextLevel();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameDifficulty.Difficulty = (int)GameDifficultyEnum.Medium;
            StateMachineService.Instance.SetDefaultState();
            Level.Instance.NextLevel();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameDifficulty.Difficulty = (int)GameDifficultyEnum.Hard;
            StateMachineService.Instance.SetDefaultState();
            Level.Instance.NextLevel();
        }
    }
    
    public void Exit()
    {
        _image.color = _fadeColor;
        _image.sprite = null;
        _textGroup.alpha = 0f;
        FadeOut();
    }

    public void FadeOut()
    {
        _animator.enabled = true;
    }

    public void OnAnimationFinished()
    {
        _animator.enabled = false;
    }
}
