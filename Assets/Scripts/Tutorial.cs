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
    Level _level;
    public static Tutorial Instance
    { get
    {
        if (instance == null)
        {
            instance = FindObjectOfType<Tutorial>();
        }
        return instance;
    } }

    void Start()
    {
        _level = Level.Instance;
    }

    void Update()
    {
        if (!IsTutorialActive) return;

        if (_level.LevelIndex == 0)
        {
            Level0();
        }
        else if (_level.LevelIndex == 1)
        {
            Level1();
        }
    }

    void Level0()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameDifficulty.Difficulty = (int)GameDifficultyEnum.Easy;
            CloseTutorial();
            _level.NextLevel();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameDifficulty.Difficulty = (int)GameDifficultyEnum.Medium;
            CloseTutorial();
            _level.NextLevel();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameDifficulty.Difficulty = (int)GameDifficultyEnum.Hard;
            CloseTutorial();
            _level.NextLevel();
        }
    }


    void Level1()
    {
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
