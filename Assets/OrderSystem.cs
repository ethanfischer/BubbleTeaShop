using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class OrderSystem : MonoBehaviour
{
    //singleton unity pattern
    private static OrderSystem _instance;
    public static OrderSystem Instance
    { get
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<OrderSystem>();
        }
        return _instance;
    } }

    [FormerlySerializedAs("_orderPrefab")]
    [SerializeField]
    OrderMB _orderMbPrefab;

    List<OrderMB> _orders = new List<OrderMB>();
    float _timer;
    bool _skipFirstFrame = true;
    
    float _addNewOrderTime => GameDifficulty.Difficulty switch
    {
        (int)GameDifficultyEnum.Easy => 45f,
        (int)GameDifficultyEnum.Medium => 30f,
        (int)GameDifficultyEnum.Hard => 15f,
        (int)GameDifficultyEnum.Testing => 1f,
        _ => 0f
    };

    void Start()
    {
        _timer = _addNewOrderTime;
    }

    void Update()
    {
        if (_timer > _addNewOrderTime)
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

    public void ClearOrders()
    {
        foreach (var order in _orders)
        {
            Debug.Log("Destroying order");
            Destroy(order.gameObject);
        }
    }
}
