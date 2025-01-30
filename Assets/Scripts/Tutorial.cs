using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    Image _image;
    [SerializeField]
    Color _fadeColor;
    [SerializeField]
    CanvasGroup _textGroup;
    [SerializeField]
    CanvasGroup _difficultyMenuGroup;
    [SerializeField]
    IngredientsInstructions _ingredientsInstructions;
    [SerializeField]
    DifficultyMenu _difficultyMenu;
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


    private Level _level;
    private Level Level
    { get
    {
        if (_level == null)
        {
            _level = Level.Instance;
        }
        return _level;
    } }

    public void ShowTutorial()
    {
        IsTutorialActive = true;
        Debug.Log("Showing tutorial");
    }

    void Update()
    {
        if (!IsTutorialActive) return;

        if (Level.LevelIndex == 0)
        {
            Level0();
        }
        else if (Level.LevelIndex == 1)
        {
            Level1();
            IsTutorialActive = !_ingredientsInstructions.DidCompleteTutorial;
        }
    }

    void Level0()
    {
        _difficultyMenuGroup.alpha = 1f;
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameDifficulty.Difficulty = (int)GameDifficultyEnum.Easy;
            CloseDifficultyMenu();
            Level.NextLevel();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameDifficulty.Difficulty = (int)GameDifficultyEnum.Medium;
            CloseDifficultyMenu();
            Level.NextLevel();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameDifficulty.Difficulty = (int)GameDifficultyEnum.Hard;
            CloseDifficultyMenu();
            Level.NextLevel();
        }
    }


    void Level1()
    {
        Debug.Log("Showing Level 1 tutorial");
        _ingredientsInstructions.ShowIngredientToKeyInstructions();
    }

    void CloseDifficultyMenu()
    {
        _image.color = _fadeColor;
        _image.sprite = null;
        _textGroup.alpha = 0f;
        _difficultyMenu.FadeOut();
        IsTutorialActive = false;
    }
}
