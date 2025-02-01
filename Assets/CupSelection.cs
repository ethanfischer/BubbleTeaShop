using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CupSelection : MonoBehaviour
{
    [SerializeField]
    GameObject _smallCup;
    [FormerlySerializedAs("_cupRegular")]
    [FormerlySerializedAs("_mediumCup")]
    [SerializeField]
    GameObject _regularCup;
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

    public void ShowCupSelection(CupSize size)
    {
        if (size == CupSize.Cup)
        {
            _regularCup.SetActive(true);
            _bigCup.SetActive(false);
        }
        else if (size == CupSize.LargeCup)
        {
            _regularCup.SetActive(true);
            _bigCup.SetActive(true);
        }
    }

    public void HideCupSelection()
    {
        _regularCup.SetActive(false);
        _bigCup.SetActive(false);
    }
}
