using UnityEngine;

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
}
