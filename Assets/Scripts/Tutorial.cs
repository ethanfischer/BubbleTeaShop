using System.Collections;
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
    [SerializeField]
    CanvasGroup _difficultyMenuGroup;
    [SerializeField]
    IngredientsInstructions _ingredientsInstructions;
    public bool IsTutorialActive
    { get;
      private set; } = true;

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

    public IEnumerator Tick()
    {
        if (!IsTutorialActive) yield return null;

        if (Level.LevelIndex == 0)
        {
            yield return Level0();
        }
        else if (Level.LevelIndex == 1)
        {
            yield return Level1();
        }
    }

    IEnumerator Level0()
    {
        _difficultyMenuGroup.alpha = 1f;
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameDifficulty.Difficulty = (int)GameDifficultyEnum.Easy;
            CloseTutorial();
            Level.NextLevel();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameDifficulty.Difficulty = (int)GameDifficultyEnum.Medium;
            CloseTutorial();
            Level.NextLevel();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameDifficulty.Difficulty = (int)GameDifficultyEnum.Hard;
            CloseTutorial();
            Level.NextLevel();
        }

        yield return null;
    }


    IEnumerator Level1()
    {
        Debug.Log("Showing Level 1 tutorial");
        //ShowIngredientToKeyInstructions
        _ingredientsInstructions.ShowIngredientToKeyInstructions();
        yield return null;
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
