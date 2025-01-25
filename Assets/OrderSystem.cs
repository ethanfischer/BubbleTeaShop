using UnityEngine;
using UnityEngine.Serialization;

public class OrderSystem : MonoBehaviour
{
    [SerializeField]
    Order _orderPrefab;
    
    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            var order = Instantiate(_orderPrefab, this.transform);
            order.Initialize("Small", "Boba", "30%", "Normal");
        }
    }
    
    void Update()
    {
    }
}
