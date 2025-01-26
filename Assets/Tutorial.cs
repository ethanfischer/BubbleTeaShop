using UnityEngine;

public class Tutorial : MonoBehaviour
{
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
            GameDifficulty.Difficulty = (int)GameDifficultyEnum.Easy;
            if (!DidCloseTutorial)
            {
                DidCloseTutorial = true;
                gameObject.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameDifficulty.Difficulty = (int)GameDifficultyEnum.Medium;
            if (!DidCloseTutorial)
            {
                DidCloseTutorial = true;
                gameObject.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameDifficulty.Difficulty = (int)GameDifficultyEnum.Hard;
            if (!DidCloseTutorial)
            {
                DidCloseTutorial = true;
                gameObject.SetActive(false);
            }
        }
    }
}
