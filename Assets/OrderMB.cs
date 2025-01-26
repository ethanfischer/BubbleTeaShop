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

    [SerializeField]
    RectTransform _timeBar;
    // public TMP_Text RemainingTimeText;
    float _initialBarWidth;

    public Order Order { get; private set; }
    const float INITIAL_TIME = 10;
    float _timeRemaining = INITIAL_TIME;

    string[] _sizeOptions = new string[]
    {
        "Small",
        "Medium",
        "Large"
    };

    string[] _bobaOptions = new string[]
    {
        "",
        "Boba",
        "Jelly"
    };

    string[] _sugarOptions = new string[]
    {
        "",
        "Less Sugar",
        "Regular Sugar",
        "Extra Sugar",
    };

    string[] _iceOptions = new string[]
    {
        "",
        "Less Ice",
        "Regular Ice",
        "Extra Ice"
    };

    string[] _extraToppingOptions = new string[]
    {
        "",
        "Cheese Foam",
    };

    void Start()
    {
        Order = new Order(
            Random.Range(0, _bobaOptions.Length),
            Random.Range(0, _iceOptions.Length),
            Random.Range(0, _sugarOptions.Length),
            Random.Range(0, _extraToppingOptions.Length));

        SetUIText();
        _initialBarWidth = _timeBar.sizeDelta.x;
    }

    void SetUIText()
    {
        BobaText.text = _bobaOptions[Order.Boba];
        IceText.text = _iceOptions[Order.Ice];
        SugarText.text = _sugarOptions[Order.Sugar];
        ExtraToppingText.text = _extraToppingOptions[Order.ExtraTopping];
        // RemainingTimeText.text = $"\nTime remaining: {_timeRemaining:0}";
    }

    void Update()
    {
        _timeRemaining -= Time.deltaTime;
        if (_timeRemaining <= 0)
        {
            FailOrder();
        }

        ShrinkTimebar();
    }
    
    void ShrinkTimebar()
    {
        // Calculate the proportion of time remaining
        float proportion = _timeRemaining / INITIAL_TIME;

        // Scale the bar width proportionally
        _timeBar.sizeDelta = new Vector2(_initialBarWidth * proportion, _timeBar.sizeDelta.y);
    }

    void FailOrder()
    {
        Debug.Log("Order failed");
        Destroy(this.gameObject);
        PopupText.Instance.GameOver();
    }

    public bool DoOrdersMatch(Order input)
    {
        var boba = input.Boba == Order.Boba;
        var ice = input.Ice == Order.Ice;
        var sugar = input.Sugar == Order.Sugar;
        var extraTopping = input.ExtraTopping == Order.ExtraTopping;

        Debug.Log($"Boba: {boba}, Ice: {ice}, Sugar: {sugar}, ExtraTopping: {extraTopping}");

        return boba
            && ice
            && sugar
            && extraTopping;
    }
}
