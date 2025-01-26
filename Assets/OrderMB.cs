using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class OrderMB : MonoBehaviour
{
    public TMP_Text BobaText;
    public TMP_Text IceText;
    public TMP_Text SugarText;
    public TMP_Text ExtraToppingText;

    [SerializeField]
    RectTransform _timeBar;
    float _initialBarWidth;

    public Order Order { get; private set; }
    const float INITIAL_TIME = 10;
    float _timeRemaining = INITIAL_TIME;
    Image _timeBarImage;

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
        _timeBarImage = _timeBar.GetComponent<Image>();
    }

    void SetUIText()
    {
        BobaText.text = _bobaOptions[Order.Boba];
        IceText.text = _iceOptions[Order.Ice];
        SugarText.text = _sugarOptions[Order.Sugar];
        ExtraToppingText.text = _extraToppingOptions[Order.ExtraTopping];
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

        _timeBarImage = _timeBar.GetComponent<Image>();

        var colorTargetTimeOffset = INITIAL_TIME * 0.5f;
        float colorProportion = Mathf.Clamp01((_timeRemaining - colorTargetTimeOffset) / (INITIAL_TIME - colorTargetTimeOffset));
        _timeBarImage.color = Color.Lerp(Color.red, Color.green, colorProportion);
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
