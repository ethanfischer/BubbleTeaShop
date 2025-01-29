using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    Animator _animator;
    [SerializeField]
    Image _image;
    public bool DidCloseTutorial { get; private set; }

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
            if (!DidCloseTutorial)
            {
                GameDifficulty.Difficulty = (int)GameDifficultyEnum.Easy;
                CloseTutorial();
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!DidCloseTutorial)
            {
                GameDifficulty.Difficulty = (int)GameDifficultyEnum.Medium;
                CloseTutorial();
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (!DidCloseTutorial)
            {
                GameDifficulty.Difficulty = (int)GameDifficultyEnum.Hard;
                CloseTutorial();
            }
        }
    }

    void CloseTutorial()
    {
        _image.color = Color.black;
        _animator.enabled = true;
        DidCloseTutorial = true;
    }

    void OnFadeOutAnimationEnd()
    {
        _animator.enabled = false;
        gameObject.SetActive(false);
    }
}
