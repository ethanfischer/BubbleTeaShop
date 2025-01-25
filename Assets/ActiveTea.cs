using DefaultNamespace;
using UnityEngine;

public class ActiveTea : MonoBehaviour
{
    [SerializeField]
    OrderSystem _orderSystem;
    
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
        throw new System.NotImplementedException();
    }

    void AddBoba()
    {
        _topping = (int)ToppingEnum.Boba;
    }

    void AddTea()
    {
        _hasTea = true;
    }

    void AddMilk()
    {
        _hasMilk = true;
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
