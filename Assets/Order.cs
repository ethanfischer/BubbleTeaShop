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
    public TMP_Text RemainingTimeText;

    int _size;
    int _toppings;
    int _sugar;
    int _ice;
    
    float _timeRemaining = 10f;

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

        SetUIText();
    }
    
    void SetUIText()
    {
        SizeText.text = $"Size: {_sizeOptions[_size]}";
        ToppingsText.text = $"Toppings: {_toppingOptions[_toppings]}";
        SugarText.text = $"Sugar: {_sugarOptions[_sugar]}";
        IceText.text = $"Ice: {_iceOptions[_ice]}";
        RemainingTimeText.text = $"Time remaining: {_timeRemaining:0}";
    }

    void Update()
    {
        _timeRemaining -= Time.deltaTime;
        if (_timeRemaining <= 0)
        {
            FailOrder();
        }
        RemainingTimeText.text = $"Time remaining: {_timeRemaining:0}";
    }

    void FailOrder()
    {
        Debug.Log("Order failed");
        Destroy(this.gameObject);
    }
}
