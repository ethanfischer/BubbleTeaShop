using System.Collections;
using System.Security.Cryptography;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class ActiveTea : MonoBehaviour
{
    [SerializeField]
    Animator _animator;
    [SerializeField]
    OrderSystem _orderSystem;

    [SerializeField]
    GameObject _activeTeaUI;
    [SerializeField]
    GameObject _teaIngredientPrefab;

    [SerializeField]
    AudioClip _typeSound;
    [SerializeField]
    AudioClip _pourSound;
    [SerializeField]
    AudioClip _iceSound;
    [SerializeField]
    AudioClip _jellySound;
    [SerializeField]
    AudioClip _correctOrderSound;
    [SerializeField]
    AudioClip _waterDropSound;
    [SerializeField]
    AudioClip _buzzerWrongSound;
    [SerializeField]
    AudioClip _splatSound;

    [SerializeField]
    AudioSource _audioSource;
    [SerializeField]
    AudioSource _secondAudioSource;

    [SerializeField]
    Transform _root;

    [SerializeField]
    Material _teaAndMilkMaterial;
    [SerializeField]
    Material _teaAndMilkMaterialMatcha;
    [SerializeField]
    Material _teaAndMilkMaterialTaro;
    [SerializeField]
    Material _teaMaterial;

    public bool HasCup { get; private set; }
    bool _hasMilk;
    bool _hasTea;
    int _cup;
    int _boba;
    int _ice;
    int _sugar;
    int _tea;
    int _extraTopping;
    GameObject _sugarCube;
    Vector3 _sugarCubeInitialPosition;
    public bool DidSelectBoba { get; private set; }
    public bool DidSelectTea { get; private set; }
    bool _skipFirstTick = true;

    [SerializeField]
    float _spinSpeed = 0.05f;
    [SerializeField]
    bool _shouldLoseIfOutOfMoney;

    [SerializeField]
    Color _defaultIceColor;
    [SerializeField]
    Color _submergedIceColor;
    [SerializeField]
    bool _shouldPopupTypedIngredients = true;

    void Start()
    {
        _sugarCube = GameObject.Find("SugarCube");
        if (_sugarCube == null) Debug.LogError("Sugar cube not found");

        _sugarCubeInitialPosition = _sugarCube.transform.position;
    }

    public void AddJelly()
    {
        _boba = (int)BobaEnum.Jelly;
        AddIngredientTextToUI("Jelly");

        _audioSource.clip = _jellySound;
        _audioSource.Play();
        _root.Find("Grass_Jelly").gameObject.SetActive(true);
        Debug.Log("Jelly added");
    }

    public void AddSugar()
    {
        _sugar++;
        AddIngredientTextToUI("Sugar");

        _audioSource.clip = _waterDropSound;
        _audioSource.Play();
        StartCoroutine(FlashSugarCube());
        Debug.Log("Sugar added");
    }

    IEnumerator FlashSugarCube()
    {
        _sugarCube.SetActive(true);

        var moveAmount = 1.7f;
        var targetPosition = _sugarCubeInitialPosition + Vector3.down * moveAmount;
        var duration = .5f;
        var elapsedTime = 0f;

        // Get the material and its color
        var cubeRenderer = _sugarCube.GetComponent<Renderer>();
        var material = cubeRenderer.material;
        var initialColor = material.color;

        while (elapsedTime < duration)
        {
            // Calculate eased progress using a quadratic easing formula for acceleration
            var t = elapsedTime / duration;
            var easedT = 1 - Mathf.Pow(1 - t, 2); // Quadratic easing-out for deceleration

            // Update position
            _sugarCube.transform.position = Vector3.Lerp(_sugarCubeInitialPosition, targetPosition, easedT);

            // Update alpha for fade-out effect
            var alpha = Mathf.Lerp(1f, 0f, t);
            material.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the cube is fully faded and at its final position
        _sugarCube.transform.position = targetPosition;
        material.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
        _sugarCube.SetActive(false);
    }

    public void AddIce()
    {
        _ice++;
        _audioSource.clip = _iceSound;
        _audioSource.Play();
        AddIngredientTextToUI("Ice");

        Debug.Log("Ice added");

        if (_ice == 1)
        {
            _root.Find("Ice_One").gameObject.SetActive(true);
        }
        else if (_ice == 2)
        {
            _root.Find("Ice_Two").gameObject.SetActive(true);
        }
        else if (_ice == 3)
        {
            _root.Find("Ice_Three").gameObject.SetActive(true);
        }

        HandleIceSubmerged();
    }

    void HandleIceSubmerged()
    {
        var iceOne = _root.Find("Ice_One").gameObject;
        var iceTwo = _root.Find("Ice_Two").gameObject;
        var iceThree = _root.Find("Ice_Three").gameObject;
        var ices = new[] { iceOne, iceTwo, iceThree };
        var milkAndTea = _root.Find("MilkAndTea").gameObject;
        var tea = _root.Find("Tea").gameObject;
        foreach (var ice in ices)
        {
            if (tea.activeSelf || milkAndTea.activeSelf)
            {
                ice.GetComponent<MeshRenderer>().material.color = _submergedIceColor;
            }
            else
            {
                ice.GetComponent<MeshRenderer>().material.color = _defaultIceColor;
            }
        }
    }

    public void AddCheeseFoam()
    {
        _extraTopping = (int)ExtraToppingEnum.CheeseFoam;
        AddIngredientTextToUI("Cheese Foam");

        _root.Find("CheeseFoamHonbComb").gameObject.SetActive(true);
        Debug.Log("Cheese Foam added");
    }


    public void AddRegularBoba()
    {
        _boba = (int)BobaEnum.BobaRegular;
        AddIngredientTextToUI("Boba");

        _audioSource.clip = _jellySound;
        _audioSource.Play();
        var bobaGameObject = _root.Find("Boba");
        bobaGameObject.gameObject.SetActive(true);
        bobaGameObject.GetComponent<MeshRenderer>().material.color = Color.black;
        DidSelectBoba = true;
        Debug.Log("Regular Boba added");
    }
    
    
    public void AddCup(CupSize size)
    {
        HasCup = true;
        // _audioSource.clip = _jellySound;
        // _audioSource.Play();
        _animator.Play("AddCup");
        _animator.enabled = true;
        var cup = _root.Find("cup");
        cup.gameObject.SetActive(true);

        transform.localScale = size switch
        {
            CupSize.LargeCup => new Vector3(1f, 1.1f, 1f),
            CupSize.Cup => Vector3.one,
            _ => transform.localScale
        };
        _cup = size switch
        {
            CupSize.LargeCup => 1,
            CupSize.Cup => 0,
            _ => 0
        };
        Debug.Log($"Cup size {_cup } added");
    }

    public void AddMangoBoba()
    {
        _boba = (int)BobaEnum.BobaMango;
        AddIngredientTextToUI("Mango Boba");
        _audioSource.clip = _jellySound;
        _audioSource.Play();
        var bobaGameObject = _root.Find("Boba");
        bobaGameObject.gameObject.SetActive(true);
        bobaGameObject.GetComponent<MeshRenderer>().material.color = Color.yellow; //TODO: Change color to match BobaMango
        DidSelectBoba = true;
        Debug.Log("Mango Boba added");
    }

    public void AddStrawberryBoba()
    {
        DidSelectBoba = true;
        _boba = (int)BobaEnum.BobaStawberry;
        AddIngredientTextToUI("Strawberry Boba");
        _audioSource.clip = _jellySound;
        _audioSource.Play();
        var bobaGameObject = _root.Find("Boba");
        bobaGameObject.gameObject.SetActive(true);
        bobaGameObject.GetComponent<MeshRenderer>().material.color = Color.red; //TODO: Change color to match BobaStrawberry
        Debug.Log("Strawberry Boba added");
    }

    public void AddBlueberryBoba()
    {
        DidSelectBoba = true;
        _boba = (int)BobaEnum.BobaBlueberry;
        AddIngredientTextToUI("Blueberry Boba");
        _audioSource.clip = _jellySound;
        _audioSource.Play();
        var bobaGameObject = _root.Find("Boba");
        bobaGameObject.gameObject.SetActive(true);
        bobaGameObject.GetComponent<MeshRenderer>().material.color = Color.blue; //TODO: Change color to match BobaBlueberry
        Debug.Log("Blueberry Boba added");
    }

    public void AddRegularTea()
    {
        _tea = (int)TeaEnum.Regular;
        _hasTea = true;
        AddIngredientTextToUI("Tea");
        var teaObject = _root.Find("Tea");
        if (_hasMilk)
        {
            var milkAndTeaObject = _root.Find("MilkAndTea").gameObject;
            milkAndTeaObject.SetActive(true);
            milkAndTeaObject.GetComponent<MeshRenderer>().material = _teaAndMilkMaterial;
            teaObject.gameObject.SetActive(false);
            _root.Find("Milk").gameObject.SetActive(false);
        }
        else
        {
            _root.Find("Tea").gameObject.SetActive(true);
            teaObject.gameObject.SetActive(true);
            teaObject.GetComponent<MeshRenderer>().material = _teaMaterial;
        }

        HandleIceSubmerged();

        _audioSource.clip = _pourSound;
        _audioSource.Play();

        Debug.Log("Tea added");
    }

    public void AddTaroTea()
    {
        _tea = (int)TeaEnum.Taro;
        _hasTea = true;
        AddIngredientTextToUI("Taro");
        _audioSource.clip = _pourSound;
        _audioSource.Play();
        var teaObject = _root.Find("Tea");
        if (_hasMilk)
        {
            var milkAndTeaObject = _root.Find("MilkAndTea").gameObject;
            milkAndTeaObject.SetActive(true);
            milkAndTeaObject.GetComponent<MeshRenderer>().material = _teaAndMilkMaterialTaro;
            teaObject.gameObject.SetActive(false);
            _root.Find("Milk").gameObject.SetActive(false);
        }
        else
        {
            _root.Find("Tea").gameObject.SetActive(true);
            teaObject.gameObject.SetActive(true);
            teaObject.GetComponent<MeshRenderer>().material = _teaAndMilkMaterialTaro; //TODO: we need a different material for taro tea without milk
        }
        Debug.Log("Taro Tea added");
    }

    public void AddMatchaTea()
    {
        _tea = (int)TeaEnum.Matcha;
        _hasTea = true;
        AddIngredientTextToUI("Matcha");
        _audioSource.clip = _pourSound;
        _audioSource.Play();
        var teaObject = _root.Find("Tea");
        if (_hasMilk)
        {
            var milkAndTeaObject = _root.Find("MilkAndTea").gameObject;
            milkAndTeaObject.SetActive(true);
            milkAndTeaObject.GetComponent<MeshRenderer>().material = _teaAndMilkMaterialMatcha;
            teaObject.gameObject.SetActive(false);
            _root.Find("Milk").gameObject.SetActive(false);
        }
        else
        {
            _root.Find("Tea").gameObject.SetActive(true);
            teaObject.gameObject.SetActive(true);
            teaObject.GetComponent<MeshRenderer>().material = _teaAndMilkMaterialMatcha; //TODO: we need a different material for matcha tea without milk
        }
        Debug.Log("Matcha Tea added");
    }


    void AddIngredientTextToUI(string text)
    {
        _secondAudioSource.clip = _typeSound;
        _secondAudioSource.Play();

        if (_shouldPopupTypedIngredients)
        {
            PopupText.Instance.ShowPopup($"<size=120>{text}</size>", 0.2f);
        }

        var ingredient = Instantiate(_teaIngredientPrefab, _activeTeaUI.transform);
        ingredient.GetComponent<TMP_Text>().text = text;
        ingredient.transform.SetSiblingIndex(0);
        ScreenShake.Instance.TriggerShake(0.02f, 2f);
    }

    int ClearIngredientUIText()
    {
        var count = 0;
        foreach (Transform child in _activeTeaUI.transform)
        {
            child.gameObject.GetComponent<ActiveIngredientText>().FadeOut();
            count++;
        }

        return count;
    }

    public void AddMilk()
    {
        _hasMilk = true;
        _audioSource.clip = _pourSound;
        _audioSource.Play();
        AddIngredientTextToUI("Milk");

        if (_hasTea)
        {
            _root.Find("MilkAndTea").gameObject.SetActive(true); //TODO: change material for taro and matcha here
            if (_tea == (int)TeaEnum.Matcha)
            {
                _root.Find("MilkAndTea").GetComponent<MeshRenderer>().material = _teaAndMilkMaterialMatcha;
            }
            else if (_tea == (int)TeaEnum.Taro)
            {
                _root.Find("MilkAndTea").GetComponent<MeshRenderer>().material = _teaAndMilkMaterialTaro;
            }
            else if (_tea == (int)TeaEnum.Regular)
            {
                _root.Find("MilkAndTea").GetComponent<MeshRenderer>().material = _teaAndMilkMaterial;
            }
            _root.Find("Tea").gameObject.SetActive(false);
            _root.Find("Milk").gameObject.SetActive(false);
        }
        else
        {
            _root.Find("Milk").gameObject.SetActive(true);
        }

        HandleIceSubmerged();
        Debug.Log("Milk added");
    }

    public void SubmitTea()
    {
        if (!_hasMilk)
        {
            PopupText.Instance.ShowPopup("Missing milk");
            _animator.Play("WrongOrderBobaCupAnimation");
            _animator.enabled = true;
            return;
        }
        if (!_hasTea)
        {
            PopupText.Instance.ShowPopup("Missing tea");
            _animator.Play("WrongOrderBobaCupAnimation");
            _animator.enabled = true;
            return;
        }
        var order = new Order(_cup, _boba, _ice, _sugar, _tea, _extraTopping);
        if (_orderSystem.TryGetMatchingOrder(order, out var matchingOrder))
        {
            HandleCorrectOrder(matchingOrder);
        }
        else
        {
            HandleWrongOrder();
        }
    }

    public void SubmitTeaForTutorial()
    {
        // _animator.Play("OrderSubmitBubbleTeaCupAnimation");
        // _animator.enabled = true;
        _audioSource.clip = _correctOrderSound;
        _audioSource.Play();
        ClearIngredientUIText();
    }

    void HandleWrongOrder()
    {
        Handheld.Vibrate();
        Handheld.Vibrate();
        Handheld.Vibrate();
        _audioSource.clip = _buzzerWrongSound;
        _audioSource.Play();
        _animator.Play("WrongOrderBobaCupAnimation");
        _animator.enabled = true;
        PopupText.Instance.ShowPopup("<color=red>X</color>", 0.75f);
    }

    void HandleCorrectOrder(OrderMB matchingOrder)
    {
        Handheld.Vibrate();
        Debug.Log("Tea submitted and matched order");
        _root.gameObject.SetActive(false);
        ScreenShake.Instance.TriggerShake(0.1f, 5f);
        matchingOrder.Complete();
        ClearIngredientUIText();
        _audioSource.clip = _correctOrderSound;
        _audioSource.Play();
        var cashColor = PopupText.Instance.GetCashColorHexCode();
        var earnings = 5m;
        if (matchingOrder.TimeRemaining < 5f)
        {
            earnings = (int)matchingOrder.TimeRemaining;
        }

        PopupText.Instance.ShowPopup($"<color=#{cashColor}>${earnings}</color>", 0.5f);
        OrderSystem.Instance.RecordFullfilledOrder(earnings);
        Reset();
    }

    public void OnOneShotAnimationFinished()
    {
        _animator.enabled = false;
    }

    public void TrashTea()
    {
        _animator.Play("TrashBobaAnimation");
        _animator.enabled = true;
        var ingredientsRemoved = ClearIngredientUIText();
        var cost = ingredientsRemoved * .50m;
        if (cost == 0)
        {
            cost = 0.01m;
        }
        PopupText.Instance.ShowPopup($"<color=red>${-cost}</color>", .5f);
        OrderSystem.Instance.RecordTrashedTea(cost);
        _audioSource.clip = _splatSound;
        _audioSource.Play();
        Debug.Log("Tea trashed");
        Reset();
        if (OrderSystem.Instance.Cash < 0 && _shouldLoseIfOutOfMoney)
        {
            OrderSystem.Instance.GameOver("<color=red>-$2.50\nYou ran out of money</color>");
        }
    }

    public void TrashTeaForTutorial()
    {
        _animator.Play("TrashBobaAnimation");
        _animator.enabled = true;
        _audioSource.clip = _splatSound;
        _audioSource.Play();
        ClearIngredientUIText();
        Reset();
    }

    void Reset()
    {
        HasCup = false;
        _hasTea = false;
        _hasMilk = false;
        _boba = 0;
        _ice = 0;
        _sugar = 0;
        _extraTopping = 0;
        DidSelectBoba = false;
        DidSelectTea = false;
        _tea = 0;
        _root.Find("Boba").gameObject.SetActive(false);
        _root.Find("Ice_One").gameObject.SetActive(false);
        _root.Find("Ice_Two").gameObject.SetActive(false);
        _root.Find("Ice_Three").gameObject.SetActive(false);
        _root.Find("Milk").gameObject.SetActive(false);
        _root.Find("Tea").gameObject.SetActive(false);
        _root.Find("CheeseFoamHonbComb").gameObject.SetActive(false);
        _root.Find("Grass_Jelly").gameObject.SetActive(false);
        _root.Find("MilkAndTea").gameObject.SetActive(false);
    }
}
