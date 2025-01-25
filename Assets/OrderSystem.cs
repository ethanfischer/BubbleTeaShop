using System.Collections.Generic;
using UnityEngine;

public class OrderSystem : MonoBehaviour
{
    [SerializeField]
    Order _orderPrefab;

    float _timer = 0f;
    const float AddNewOrderTime = 5f;

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > AddNewOrderTime)
        {
            _timer = 0f;
            Instantiate(_orderPrefab, this.transform);
        }
    }
}
