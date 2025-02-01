using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupSelection : MonoBehaviour
{
    [SerializeField]
    GameObject _smallCup;
    [SerializeField]
    GameObject _mediumCup;
    [SerializeField]
    GameObject _bigCup;

    //unity singleton pattern
    private static CupSelection _instance;
    public static CupSelection Instance
    { get
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<CupSelection>();
        }

        return _instance;
    } }

    public void ShowCupSelection()
    {
        _mediumCup.SetActive(true);
        _bigCup.SetActive(true);
    }

    public void HideCupSelection()
    {
        _mediumCup.SetActive(false);
        _bigCup.SetActive(false);
    }
}
