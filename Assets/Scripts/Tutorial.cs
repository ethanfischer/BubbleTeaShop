using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    Animator _animator;
    [SerializeField]
    Image _image;
    [SerializeField]
    Color _fadeColor;
    [SerializeField]
    CanvasGroup _textGroup;
    public bool IsTutorialActive { get; private set; } = true;

    //singleton unity pattern
    private static Tutorial instance;
    public static Tutorial Instance
    { get
    {
        if (instance == null)
        {
            instance = FindObjectOfType<Tutorial>();
        }
        return instance;
    } }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (IsTutorialActive)
            {
                GameDifficulty.Difficulty = (int)GameDifficultyEnum.Easy;
                CloseTutorial();
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (IsTutorialActive)
            {
                GameDifficulty.Difficulty = (int)GameDifficultyEnum.Medium;
                CloseTutorial();
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (IsTutorialActive)
            {
                GameDifficulty.Difficulty = (int)GameDifficultyEnum.Hard;
                CloseTutorial();
            }
        }
    }

    void CloseTutorial()
    {
        _image.color = _fadeColor;
        _image.sprite = null;
        _textGroup.alpha = 0f;
        _animator.enabled = true;
        IsTutorialActive = false;
    }

    void OnFadeOutAnimationEnd()
    {
        _animator.enabled = false;
        gameObject.SetActive(false);
    }
}
