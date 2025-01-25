using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class OrderSystem : MonoBehaviour
{
    [FormerlySerializedAs("_orderPrefab")]
    [SerializeField]
    OrderMB _orderMbPrefab;

    List<OrderMB> _orders = new List<OrderMB>();
    const float ADD_NEW_ORDER_TIME = 20f;
    float _timer = ADD_NEW_ORDER_TIME;

    void Update()
    {
        if (_timer > ADD_NEW_ORDER_TIME)
        {
            _timer = 0f;
            _orders.Add(Instantiate(_orderMbPrefab, this.transform));
        }
        _timer += Time.deltaTime;
    }

    public bool TryGetMatchingOrder(Order input, out OrderMB matchingOrderMb)
    {
        matchingOrderMb = _orders.FirstOrDefault(o => o.DoOrdersMatch(input));
        if (matchingOrderMb != null)
        {
            PopupText.Instance.ShowPopup("Matching order found");
            Debug.Log("Matching order found");
            return true;
        }

        PopupText.Instance.ShowPopup("No matching order found");
        return false;
    }
}
