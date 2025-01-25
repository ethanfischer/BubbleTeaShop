using UnityEngine;

public class OrderSystem : MonoBehaviour
{
    [SerializeField]
    Order _orderPrefab;

    const float ADD_NEW_ORDER_TIME = 45f;
    float _timer = ADD_NEW_ORDER_TIME;

    void Update()
    {
        if (_timer > ADD_NEW_ORDER_TIME)
        {
            _timer = 0f;
            Instantiate(_orderPrefab, this.transform);
        }
        _timer += Time.deltaTime;
    }
}
