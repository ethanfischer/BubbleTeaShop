using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class OrderMB : MonoBehaviour
{
    public TMP_Text BobaText;
    public TMP_Text IceText;
    public TMP_Text SugarText;
    public TMP_Text ExtraToppingText;
    public TMP_Text RemainingTimeText;

    public Order Order { get; private set; }
    float _timeRemaining = 60f;

    string[] _sizeOptions = new string[]
    {
        "Small",
        "Medium",
        "Large"
    };

    string[] _bobaOptions = new string[]
    {
        "None",
        "Boba",
        "Jelly"
    };

    string[] _sugarOptions = new string[]
    {
        "None",
        "Less",
        "Regular",
        "Extra%",
    };

    string[] _iceOptions = new string[]
    {
        "None",
        "Less",
        "Normal",
        "Extra"
    };
    
    string[] _extraToppingOptions = new string[]
    {
        "None",
        "CheeseFoam",
    };

    void Start()
    {
        Order = new Order(
            Random.Range(0, _bobaOptions.Length),
            Random.Range(0, _iceOptions.Length),
            Random.Range(0, _sugarOptions.Length),
            Random.Range(0, _extraToppingOptions.Length));

        SetUIText();
    }

    void SetUIText()
    {
        BobaText.text = $"Boba: {_bobaOptions[Order.Boba]}";
        IceText.text = $"Ice: {_iceOptions[Order.Ice]}";
        SugarText.text = $"Sugar: {_sugarOptions[Order.Sugar]}";
        ExtraToppingText.text = $"Extra Toppings: {_extraToppingOptions[Order.ExtraTopping]}";
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

    public bool DoOrdersMatch(Order input)
    {
        return input.Boba == Order.Boba
            && input.Ice == Order.Ice
            && input.Sugar == Order.Sugar
            && input.ExtraTopping == Order.ExtraTopping;
    }
}
