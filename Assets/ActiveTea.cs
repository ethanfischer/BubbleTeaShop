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
        if (Input.GetKeyDown(KeyCode.Space))
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
        AddIngredientTextToUI("Ice");
        Debug.Log("Ice added");
    }

    void AddCheeseFoam()
    {
        _extraTopping = (int)ExtraToppingEnum.CheeseFoam;
        AddIngredientTextToUI("Cheese Foam");
        Debug.Log("Cheese Foam added");
    }

    void AddBoba()
    {
        _boba = (int)BobaEnum.Boba;
        AddIngredientTextToUI("Boba");
        Debug.Log("Boba added");
    }

    void AddTea()
    {
        _hasTea = true;
        AddIngredientTextToUI("Tea");
        Debug.Log("Tea added");
    }

    void AddIngredientTextToUI(string tea)
    {
        var ingredient = Instantiate(_teaIngredientPrefab, _activeTeaUI.transform);
        ingredient.GetComponent<TMP_Text>().text = tea;
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
        AddIngredientTextToUI("Milk");
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
            Destroy(matchingOrder.gameObject);
            ClearIngredientUIText();
            PopupText.Instance.ShowPopup("Order matched");
            Debug.Log("Tea submitted and matched order");
            Reset();
        }
        else
        {
            PopupText.Instance.ShowPopup("No matching order");
        }
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
    }
}
