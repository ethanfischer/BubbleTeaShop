using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Order : MonoBehaviour
{
    public TMP_Text SizeText;
    public TMP_Text ToppingsText;
    public TMP_Text SugarText;
    public TMP_Text IceText;

    int _size;
    int _toppings;
    int _sugar;
    int _ice;

    string[] _sizeOptions = new string[]
    {
        "Small",
        "Medium",
        "Large"
    };

    string[] _toppingOptions = new string[]
    {
        "None",
        "Boba",
        "Jelly"
    };
    
    string[] _sugarOptions = new string[]
    {
        "None",
        "30%",
        "50%",
        "75%",
        "100%"
    };
    
    string[] _iceOptions = new string[]
    {
        "None",
        "Less",
        "Normal",
        "Extra"
    };

    void Start()
    {
        _size = Random.Range(0, _sizeOptions.Length);
        _toppings = Random.Range(0, _toppingOptions.Length);
        _sugar = Random.Range(0, _sugarOptions.Length);
        _ice = Random.Range(0, _iceOptions.Length);

        SizeText.text = $"Size: {_sizeOptions[_size]}";
        ToppingsText.text = $"Toppings: {_toppingOptions[_toppings]}";
        SugarText.text = $"Sugar: {_sugarOptions[_sugar]}";
        IceText.text = $"Ice: {_iceOptions[_ice]}";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
