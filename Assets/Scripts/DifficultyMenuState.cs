using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
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
    [SerializeField]
    TMP_Text _highScoreText;

    public void Enter()
    {
        _canvasGroup.alpha = 1f; //Show
        var highScore = HighScore.Instance.GetHighScore();
        if (highScore > 0)
        {
            _highScoreText.text = $"High Score: ${highScore:N2}";
        }
    }

    void IState.Tick()
    {
        _canvasGroup.alpha = 1f;
        if (DidPressKey(KeyCode.E))
        {
            GameDifficulty.Difficulty = (int)GameDifficultyEnum.Easy;
            StateMachineService.Instance.SetDefaultState();
            Level.Instance.NextLevel();
        }
        if (DidPressKey(KeyCode.M))
        {
            GameDifficulty.Difficulty = (int)GameDifficultyEnum.Medium;
            StateMachineService.Instance.SetDefaultState();
            Level.Instance.NextLevel();
        }
        if (DidPressKey(KeyCode.H))
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
        _highScoreText.text = string.Empty;
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

    bool DidPressKey(KeyCode keyCode) => NativeKeyboardHandler.KeyCode == keyCode;
}
