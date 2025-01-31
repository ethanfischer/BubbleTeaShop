using System.Collections;
using System.Linq;
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

    public bool[] CompletedTutorials { get; private set; } =
    {
        false,
        false,
        true, //Level 2 doesn't need a tutorial
        false,
        false,
        false
    };

    public void ShowTutorial()
    {
        IsTutorialActive = true;
        Debug.Log("Showing tutorial");
    }

    void Update()
    {
        if (!IsTutorialActive) return;

        if (GameDifficulty.Difficulty == (int)GameDifficultyEnum.None)
        {
            DifficultyMenu();
        }
        else 
        {
            ShowIngredientToKeyInstructions(Level.LevelIndex);

            if (_ingredientsInstructions.DidCompleteTutorial)
            {
                IsTutorialActive = false;
                CompletedTutorials[Level.LevelIndex] = true;
            }
        }
    }

    void DifficultyMenu()
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

    void ShowIngredientToKeyInstructions(int i)
    {
        var order = OrderSystem.Instance.Orders.First();
        _ingredientsInstructions.ShowIngredientToKeyInstructions(order, i);
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
