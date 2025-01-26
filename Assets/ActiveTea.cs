using DefaultNamespace;
using TMPro;
using UnityEngine;

public class ActiveTea : MonoBehaviour
{
    [SerializeField]
    OrderSystem _orderSystem;

    [SerializeField]
    GameObject _activeTeaUI;
    [SerializeField]
    GameObject _teaIngredientPrefab;

    [SerializeField]
    AudioClip _pourSound;
    [SerializeField]
    AudioClip _iceSound;
    [SerializeField]
    AudioClip _jellySound;
    [SerializeField]
    AudioClip _correctOrderSound;

    [SerializeField]
    AudioSource _audioSource;

    bool _hasMilk;
    bool _hasTea;
    int _boba;
    int _ice;
    int _sugar;
    int _extraTopping;

    void Update()
    {
        //Boba
        if (Input.GetKeyDown(KeyCode.B))
        {
            AddBoba();
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
            AddTea();
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
        Debug.Log("Sugar added");
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
    }

    void AddCheeseFoam()
    {
        _extraTopping = (int)ExtraToppingEnum.CheeseFoam;
        AddIngredientTextToUI("Cheese Foam");
        transform.Find("CheeseFoamHonbComb").gameObject.SetActive(true);
        Debug.Log("Cheese Foam added");
    }

    void AddBoba()
    {
        _boba = (int)BobaEnum.Boba;
        AddIngredientTextToUI("Boba");
        _audioSource.clip = _jellySound;
        _audioSource.Play();
        transform.Find("Boba").gameObject.SetActive(true);
        Debug.Log("Boba added");
    }

    void AddTea()
    {
        _hasTea = true;
        AddIngredientTextToUI("Tea");
        transform.Find("Tea").gameObject.SetActive(true);
        Debug.Log("Tea added");
    }

    void AddIngredientTextToUI(string tea)
    {
        var ingredient = Instantiate(_teaIngredientPrefab, _activeTeaUI.transform);
        ingredient.GetComponent<TMP_Text>().text = tea;
        ingredient.transform.SetSiblingIndex(0);
    }

    void ClearIngredientUIText()
    {
        foreach (Transform child in _activeTeaUI.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void AddMilk()
    {
        _hasMilk = true;
        _audioSource.clip = _pourSound;
        _audioSource.Play();
        AddIngredientTextToUI("Milk");
        transform.Find("Milk").gameObject.SetActive(true);
        Debug.Log("Milk added");
    }

    void SubmitTea()
    {
        if (!_hasMilk)
        {
            PopupText.Instance.ShowPopup("Missing milk");
            return;
        }
        if (!_hasTea)
        {
            PopupText.Instance.ShowPopup("Missing tea");
            return;
        }
        var order = new Order(_boba, _ice, _sugar, _extraTopping);
        if (_orderSystem.TryGetMatchingOrder(order, out var matchingOrder))
        {
            HandleCorrectOrder(matchingOrder);
        }
        else
        {
            PopupText.Instance.ShowPopup("No matching order");
        }
    }
    
    void HandleCorrectOrder(OrderMB matchingOrder)
    {
        Destroy(matchingOrder.gameObject);
        ClearIngredientUIText();
        PopupText.Instance.ShowPopup("Good", 0.5f);
        Debug.Log("Tea submitted and matched order");
        _audioSource.clip = _correctOrderSound;
        _audioSource.Play();
        Reset();
    }

    void TrashTea()
    {
        ClearIngredientUIText();
        PopupText.Instance.ShowPopup("Tea trashed");
        Debug.Log("Tea trashed");
        Reset();
    }

    void Reset()
    {
        _hasTea = false;
        _hasMilk = false;
        _boba = 0;
        _ice = 0;
        _sugar = 0;
        _extraTopping = 0;
        transform.Find("Boba").gameObject.SetActive(false);
        transform.Find("Ice_One").gameObject.SetActive(false);
        transform.Find("Ice_Two").gameObject.SetActive(false);
        transform.Find("Ice_Three").gameObject.SetActive(false);
        transform.Find("Milk").gameObject.SetActive(false);
        transform.Find("Tea").gameObject.SetActive(false);
        transform.Find("CheeseFoamHonbComb").gameObject.SetActive(false);
        transform.Find("Grass_Jelly").gameObject.SetActive(false);
    }
}
