using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class OrderMB : MonoBehaviour
{
    public TMP_Text SizeText;
    public TMP_Text ToppingsText;
    public TMP_Text SugarText;
    public TMP_Text IceText;
    public TMP_Text RemainingTimeText;

    public Order Order { get; private set; }
    float _timeRemaining = 60f;

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
        Order = new Order(
            Random.Range(0, _sizeOptions.Length),
            Random.Range(0, _toppingOptions.Length),
            Random.Range(0, _sugarOptions.Length),
            Random.Range(0, _iceOptions.Length));

        SetUIText();
    }

    void SetUIText()
    {
        SizeText.text = $"Size: {_sizeOptions[Order.Size]}";
        ToppingsText.text = $"Toppings: {_toppingOptions[Order.Topping]}";
        SugarText.text = $"Sugar: {_sugarOptions[Order.Sugar]}";
        IceText.text = $"Ice: {_iceOptions[Order.Ice]}";
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
        return input.Size == Order.Size
            && input.Topping == Order.Topping
            && input.Sugar == Order.Sugar
            && input.Ice == Order.Ice;
    }
}
