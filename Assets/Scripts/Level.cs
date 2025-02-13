using UnityEngine;
using Input = NativeKeyboardHandler;

public class Level : MonoBehaviour
{
    [SerializeField]
    GameObject _taroTeaBowl;
    [SerializeField]
    GameObject _matchaTeaBowl;
    [SerializeField]
    GameObject _mangoBobaBowl;
    [SerializeField]
    GameObject _blueberryBobaBowl;
    [SerializeField]
    GameObject _strawberryBobaBowl;

    public int LevelIndex = 0;
    private static Level _instance;
    public static Level Instance
    { get
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<Level>();
        }
        return _instance;
    } }

    public void NextLevel()
    {
        LevelIndex++;
        PopupText.Instance.ShowPopup("Level " + LevelIndex);
        Debug.Log("Next level");

        if (LevelIndex >= 5)
        {
            _mangoBobaBowl.SetActive(true);
            _blueberryBobaBowl.SetActive(true);
            _strawberryBobaBowl.SetActive(true);
        }
        if (LevelIndex >= 6)
        {
            _matchaTeaBowl.SetActive(true);
            _taroTeaBowl.SetActive(true);
        }
    }

    void Update()
    {
        switch (Input.KeyCode)
        {
            case KeyCode.Alpha1:
                GoToLevel(1);
                break;
            case KeyCode.Alpha2:
                GoToLevel(2);
                break;
            case KeyCode.Alpha3:
                GoToLevel(3);
                break;
            case KeyCode.Alpha4:
                GoToLevel(4);
                break;
            case KeyCode.Alpha5:
                GoToLevel(5);
                break;
            case KeyCode.Alpha6:
                GoToLevel(6);
                break;
            case KeyCode.Alpha7:
                GoToLevel(7);
                break;
            case KeyCode.Alpha8:
                GoToLevel(8);
                break;
            case KeyCode.Alpha9:
                GoToLevel(9);
                break;
            case KeyCode.Alpha0:
                GoToLevel(0);
                break;
            default:
                break;
        }
    }
    
    void GoToLevel(int levelIndex)
    {
        OrderSystem.Instance.Reset();
        StateMachineService.Instance.SetDefaultState();
        LevelIndex = levelIndex-1;
        NextLevel();
    }
}
