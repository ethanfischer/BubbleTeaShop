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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!DidCloseTutorial)
            {
                DidCloseTutorial = true;
                gameObject.SetActive(false);
            }
        }
    }
}
