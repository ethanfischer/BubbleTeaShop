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
    Texture _teaRegularTexture;
    [SerializeField]
    Texture _teaTaroTexture;
    [SerializeField]
    Texture _teaMatchaTexture;

    bool _hasMilk;
    bool _hasTea;
    int _boba;
    int _ice;
    int _sugar;
    int _tea;
    int _extraTopping;
    GameObject _sugarCube;
    Vector3 _sugarCubeInitialPosition;
    bool _didSelectBoba;
    bool _didSelectTea;
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
        StartCoroutine(Tick());
    }

    IEnumerator Tick()
    {
        while (true)
        {
            if (!Tutorial.Instance.DidCloseTutorial)
            {
                yield return null;

                continue;
            }
            else if (_skipFirstTick)
            {
                _skipFirstTick = false;

                yield return null;

                continue;
            }

            transform.Rotate(0, _spinSpeed, 0);

            //Boba
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (Level.Instance.LevelIndex < 4)
                {
                    AddRegularBoba();
                }
                else
                {
                    if (_didSelectBoba)
                    {
                        PopupText.Instance.ShowPopup("Boba already selected", 1f);
                    }
                    else
                    {
                        yield return StartCoroutine(BobaCoroutine());
                        yield return null;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                AddJelly();
            }

            //Ice
            if (Input.GetKeyDown(KeyCode.I))
            {
                AddIceScoop();
            }

            //Milk
            if (Input.GetKeyDown(KeyCode.M))
            {
                AddMilk();
            }

            //Sugar
            if (Input.GetKeyDown(KeyCode.S))
            {
                AddSugar();
            }

            //Tea
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (Level.Instance.LevelIndex < 5)
                {
                    AddRegularTea();
                }
                else
                {
                    if (_didSelectTea)
                    {
                        PopupText.Instance.ShowPopup("Tea already selected", 1f);
                    }
                    else
                    {
                        yield return StartCoroutine(TeaCoroutine());
                        yield return null;
                    }
                }
            }


            //Toppings
            if (Input.GetKeyDown(KeyCode.C))
            {
                AddCheeseFoam();
            }

            //Submit
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                SubmitTea();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                TrashTea();
            }

            yield return null;
        }
    }

    void AddJelly()
    {
        _boba = (int)BobaEnum.Jelly;
        AddIngredientTextToUI("Jelly");

        _audioSource.clip = _jellySound;
        _audioSource.Play();
        transform.Find("Grass_Jelly").gameObject.SetActive(true);
        Debug.Log("Jelly added");
    }

    void AddSugar()
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

    void AddIceScoop()
    {
        _ice++;
        _audioSource.clip = _iceSound;
        _audioSource.Play();
        AddIngredientTextToUI("Ice");

        Debug.Log("Ice added");

        if (_ice == 1)
        {
            transform.Find("Ice_One").gameObject.SetActive(true);
        }
        else if (_ice == 2)
        {
            transform.Find("Ice_Two").gameObject.SetActive(true);
        }
        else if (_ice == 3)
        {
            transform.Find("Ice_Three").gameObject.SetActive(true);
        }

        HandleIceSubmerged();
    }

    void HandleIceSubmerged()
    {
        var iceOne = transform.Find("Ice_One").gameObject;
        var iceTwo = transform.Find("Ice_Two").gameObject;
        var iceThree = transform.Find("Ice_Three").gameObject;
        var ices = new[] { iceOne, iceTwo, iceThree };
        var milkAndTea = transform.Find("MilkAndTea").gameObject;
        var tea = transform.Find("Tea").gameObject;
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

    void AddCheeseFoam()
    {
        _extraTopping = (int)ExtraToppingEnum.CheeseFoam;
        AddIngredientTextToUI("Cheese Foam");

        transform.Find("CheeseFoamHonbComb").gameObject.SetActive(true);
        Debug.Log("Cheese Foam added");
    }

    IEnumerator BobaCoroutine()
    {
        CameraManager.Instance.ActivateBobaPose();
        while (!_didSelectBoba)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.R))
            {
                _didSelectBoba = true;
                AddRegularBoba();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                _didSelectBoba = true;
                _boba = (int)BobaEnum.BobaMango;
                AddIngredientTextToUI("Mango Boba");
                _audioSource.clip = _jellySound;
                _audioSource.Play();
                var bobaGameObject = transform.Find("Boba");
                bobaGameObject.gameObject.SetActive(true);
                bobaGameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
                Debug.Log("Mango Boba added");
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                _didSelectBoba = true;
                _boba = (int)BobaEnum.BobaStawberry;
                AddIngredientTextToUI("Strawberry Boba");
                _audioSource.clip = _jellySound;
                _audioSource.Play();
                var bobaGameObject = transform.Find("Boba");
                bobaGameObject.gameObject.SetActive(true);
                bobaGameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                Debug.Log("Strawberry Boba added");
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                _didSelectBoba = true;
                _boba = (int)BobaEnum.BobaBlueberry;
                AddIngredientTextToUI("Blueberry Boba");
                _audioSource.clip = _jellySound;
                _audioSource.Play();
                var bobaGameObject = transform.Find("Boba");
                bobaGameObject.gameObject.SetActive(true);
                bobaGameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                Debug.Log("Blueberry Boba added");
            }
        }

        CameraManager.Instance.ActivateDefaultPose();
    }

    IEnumerator TeaCoroutine()
    {
        CameraManager.Instance.ActivateTeaPose();
        while (!_didSelectTea)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.R))
            {
                _didSelectTea = true;
                _tea = (int)TeaEnum.Regular;
                AddRegularTea();
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                _didSelectTea = true;
                _tea = (int)TeaEnum.Taro;
                AddIngredientTextToUI("Taro");
                _audioSource.clip = _pourSound;
                _audioSource.Play();
                var teaObject = transform.Find("Tea");
                teaObject.gameObject.SetActive(true);
                teaObject.GetComponent<MeshRenderer>().material.mainTexture = _teaTaroTexture;
                _hasTea = true;
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                _didSelectTea = true;
                _tea = (int)TeaEnum.Matcha;
                AddIngredientTextToUI("Matcha");
                _audioSource.clip = _pourSound;
                _audioSource.Play();
                var teaObject = transform.Find("Tea");
                teaObject.gameObject.SetActive(true);
                teaObject.GetComponent<MeshRenderer>().material.mainTexture = _teaMatchaTexture;
                _hasTea = true;
            }
        }

        CameraManager.Instance.ActivateDefaultPose();
    }

    void AddRegularBoba()
    {
        _boba = (int)BobaEnum.BobaRegular;
        AddIngredientTextToUI("Regular Boba");

        _audioSource.clip = _jellySound;
        _audioSource.Play();
        var bobaGameObject = transform.Find("Boba");
        bobaGameObject.gameObject.SetActive(true);
        bobaGameObject.GetComponent<MeshRenderer>().material.color = new Color(0.4f, 0.2f, 0.1f);
        Debug.Log("Regular Boba added");
    }

    void AddRegularTea()
    {
        _hasTea = true;
        AddIngredientTextToUI("Tea");
        var teaObject = transform.Find("Tea");
        if (_hasMilk)
        {
            transform.Find("MilkAndTea").gameObject.SetActive(true);
            teaObject.gameObject.SetActive(false);
            transform.Find("Milk").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("Tea").gameObject.SetActive(true);
            teaObject.gameObject.SetActive(true);
            teaObject.GetComponent<MeshRenderer>().material.mainTexture = _teaRegularTexture;
        }

        HandleIceSubmerged();

        _audioSource.clip = _pourSound;
        _audioSource.Play();

        Debug.Log("Tea added");
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

    void AddMilk()
    {
        _hasMilk = true;
        _audioSource.clip = _pourSound;
        _audioSource.Play();
        AddIngredientTextToUI("Milk");

        if (_hasTea)
        {
            transform.Find("MilkAndTea").gameObject.SetActive(true);
            transform.Find("Tea").gameObject.SetActive(false);
            transform.Find("Milk").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("Milk").gameObject.SetActive(true);
        }

        HandleIceSubmerged();
        Debug.Log("Milk added");
    }

    void SubmitTea()
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
        var order = new Order(_boba, _ice, _sugar, _tea, _extraTopping);
        if (_orderSystem.TryGetMatchingOrder(order, out var matchingOrder))
        {
            HandleCorrectOrder(matchingOrder);
        }
        else
        {
            HandleWrongOrder();
        }
    }

    void HandleWrongOrder()
    {
        _audioSource.clip = _buzzerWrongSound;
        _audioSource.Play();
        _animator.Play("WrongOrderBobaCupAnimation");
        _animator.enabled = true;
        PopupText.Instance.ShowPopup("<color=red>X</color>", 0.75f);
    }

    void HandleCorrectOrder(OrderMB matchingOrder)
    {
        Debug.Log("Tea submitted and matched order");
        _animator.Play("OrderSubmitBubbleTeaCupAnimation");
        _animator.enabled = true;
        ScreenShake.Instance.TriggerShake(0.1f, 5f);
        OrderSystem.Instance.RemoveOrderFromList(matchingOrder);
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

    void TrashTea()
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

    void Reset()
    {
        _hasTea = false;
        _hasMilk = false;
        _boba = 0;
        _ice = 0;
        _sugar = 0;
        _extraTopping = 0;
        _didSelectBoba = false;
        _didSelectTea = false;
        _tea = 0;
        transform.Find("Boba").gameObject.SetActive(false);
        transform.Find("Ice_One").gameObject.SetActive(false);
        transform.Find("Ice_Two").gameObject.SetActive(false);
        transform.Find("Ice_Three").gameObject.SetActive(false);
        transform.Find("Milk").gameObject.SetActive(false);
        transform.Find("Tea").gameObject.SetActive(false);
        transform.Find("CheeseFoamHonbComb").gameObject.SetActive(false);
        transform.Find("Grass_Jelly").gameObject.SetActive(false);
        transform.Find("MilkAndTea").gameObject.SetActive(false);
    }
}
