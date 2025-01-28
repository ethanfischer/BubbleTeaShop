using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class OrderMB : MonoBehaviour
{
    [FormerlySerializedAs("BobaText")]
    public Image BobaImage;
    [FormerlySerializedAs("IceText")]
    public Image IceImage;
    [FormerlySerializedAs("SugarText")]
    public Image SugarImage;
    [FormerlySerializedAs("ExtraToppingText")]
    public Image ExtraToppingImage;


    [SerializeField]
    RectTransform _timeBar;
    float _initialBarHeight;

    public Order Order { get; private set; }

    float InitialTime => GameDifficulty.Difficulty switch
    {
        (int)GameDifficultyEnum.Easy => 45f,
        (int)GameDifficultyEnum.Medium => 30f,
        (int)GameDifficultyEnum.Hard => 15f,
        (int)GameDifficultyEnum.Testing => 5f,
        _ => 0f
    };

    float _timeRemaining;
    Image _timeBarImage;
    Color _timeBarColor;
    [SerializeField]
    Color _timeBarEndColor;


    bool _skipFirstFrame = true;

    void Start()
    {
        _timeRemaining = InitialTime;
        Order = OrderFactory.CreateOrder();
        SetIngredientImages();
        _initialBarHeight = _timeBar.sizeDelta.y;
        _timeBarImage = _timeBar.GetComponent<Image>();
        _timeBarColor = _timeBarImage.color;
    }

    void SetIngredientImages()
    {
        var bobaSpriteName = OrderFactory.GetBobaText(Order.Boba);
        var iceSpriteName = OrderFactory.GetIceText(Order.Ice);
        var sugarSpriteName = OrderFactory.GetSugarText(Order.Sugar);
        var extraToppingSpriteName = OrderFactory.GetExtraToppingText(Order.ExtraTopping);

        var iconManager = IconManager.Instance;


        BobaImage.GetComponent<Image>().enabled = true;
        IceImage.GetComponent<Image>().enabled = true;
        SugarImage.GetComponent<Image>().enabled = true;
        ExtraToppingImage.GetComponent<Image>().enabled = true;

        //Boba
        if (bobaSpriteName == "Strawberry Boba")
        {
            BobaImage.sprite = iconManager.StrawberryBobaSprite;
        }
        else if (bobaSpriteName == "Blueberry Boba")
        {
            BobaImage.sprite = iconManager.BlueberryBobaSprite;
        }
        else if (bobaSpriteName == "Mango Boba")
        {
            BobaImage.sprite = iconManager.MangoBobaSprite;
        }
        else if (bobaSpriteName == "Grass Jelly")
        {
            BobaImage.sprite = iconManager.GrassJellySprite;
        }
        else if (bobaSpriteName == "Regular Boba")
        {
            BobaImage.sprite = iconManager.RegularBobaSprite;
        }
        else
        {
            BobaImage.sprite = iconManager.NoBobaSprite;
        }

        //Ice
        if (iceSpriteName == "Extra Ice")
        {
            IceImage.sprite = iconManager.IceSprite3;
        }
        else if (iceSpriteName == "Regular Ice")
        {
            IceImage.sprite = iconManager.IceSprite2;
        }
        else if (iceSpriteName == "Less Ice")
        {
            IceImage.sprite = iconManager.IceSprite;
        }
        else
        {
            IceImage.sprite = iconManager.NoIceSprite;
        }

        //Sugar
        if (sugarSpriteName == "Extra Sugar")
        {
            SugarImage.sprite = iconManager.SugarSprite3;
        }
        else if (sugarSpriteName == "Regular Sugar")
        {
            SugarImage.sprite = iconManager.SugarSprite2;
        }
        else if (sugarSpriteName == "Less Sugar")
        {
            SugarImage.sprite = iconManager.SugarSprite;
        }
        else
        {
            SugarImage.sprite = iconManager.NoSugarSprite;
        }

        //ExtraTopping
        if (extraToppingSpriteName == "Cheese Foam")
        {
            ExtraToppingImage.sprite = iconManager.CheeseFoamSprite;
        }
        else
        {
            ExtraToppingImage.GetComponent<Image>().enabled = false;
            ExtraToppingImage.sprite = iconManager.CheeseFoamSprite;
        }
    }

    void Update()
    {
        if (!Tutorial.Instance.DidCloseTutorial) return;

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
        float proportion = _timeRemaining / InitialTime;

        // Scale the bar width proportionally
        _timeBar.sizeDelta = new Vector2(_timeBar.sizeDelta.x, _initialBarHeight * proportion);

        _timeBarImage = _timeBar.GetComponent<Image>();

        var colorTargetTimeOffset = InitialTime * 0.5f;
        float colorProportion = Mathf.Clamp01((_timeRemaining - colorTargetTimeOffset) / (InitialTime - colorTargetTimeOffset));
        _timeBarImage.color = Color.Lerp(_timeBarEndColor, _timeBarColor, colorProportion);
    }

    void FailOrder()
    {
        //One strike you're out
        OrderSystem.Instance.GameOver();
        Destroy(this.gameObject);
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
