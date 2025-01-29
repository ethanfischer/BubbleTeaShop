using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
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
    }
}
