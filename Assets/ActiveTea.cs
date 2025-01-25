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
    int _size;
    int _topping;
    int _sugar;
    int _ice;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            AddMilk();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            AddTea();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            AddBoba();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            AddJelly();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SubmitTea();
        }
    }
    
    void AddJelly()
    {
        _topping = (int)ToppingEnum.Jelly;
        AddIngredientTextToUI("Jelly");
        Debug.Log("Jelly added");
    }

    void AddBoba()
    {
        _topping = (int)ToppingEnum.Boba;
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

    void AddMilk()
    {
        _hasMilk = true;
        AddIngredientTextToUI("Milk");
        Debug.Log("Milk added");
    }

    void SubmitTea()
    {
        if(_hasMilk && _hasTea)
        {
            var o = new Order(_size, _topping, _sugar, _ice);
            var order = _orderSystem.TryGetMatchingOrder(o, out var matchingOrder);
            if (order != null)
            {
                Debug.Log("Tea submitted and matched order");
            }
        }
        else
        {
            Debug.Log("Missing milk or tea");
        }
    }
}
