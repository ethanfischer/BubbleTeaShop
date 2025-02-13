using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class OrderSystem : MonoBehaviour
{
    [SerializeField]
    AudioClip _orderSound;
    [SerializeField]
    AudioSource _orderAudioSource;

    [SerializeField]
    TMP_Text _cashText;
    public decimal Cash = Decimal.Zero;

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

    public List<OrderMB> Orders { get; } = new List<OrderMB>();
    public int OrdersFullfilled { get; private set; }
    float _timer;
    bool _skipFirstFrame = true;

    float _nextOrderTime;
    bool _isGameOver;

    const int NEXT_LEVEL_THRESHOLD = 10;

    [SerializeField]
    Transform _secondaryOrders;

    float GetNextOrderTime()
    {
        var lowerLimit = GameDifficulty.Difficulty switch
        {
            (int)GameDifficultyEnum.Easy => 3f,
            (int)GameDifficultyEnum.Medium => 2f,
            (int)GameDifficultyEnum.Hard => 1f,
            (int)GameDifficultyEnum.Testing => 100f,
            _ => 0f
        };
        var upperLimit = GameDifficulty.Difficulty switch
        {
            (int)GameDifficultyEnum.Easy => 15,
            (int)GameDifficultyEnum.Medium => 10f,
            (int)GameDifficultyEnum.Hard => 10f,
            (int)GameDifficultyEnum.Testing => 1f,
            _ => 0f
        };

        upperLimit -= OrdersFullfilled;
        if (upperLimit <= 1)
        {
            upperLimit = 1;
        }

        var result = Random.Range(lowerLimit, upperLimit);

        return result;
    }

    void Start()
    {
        _nextOrderTime = 2f;
        _timer = 0f;
    }

    public void Tick()
    {
        if (_isGameOver) return;

        foreach (var order in Orders)
        {
            order.Tick();
        }

        if (_timer > _nextOrderTime)
        {
            HandleNextOrder();
        }

        _timer += Time.deltaTime;
    }

    void HandleNextOrder()
    {
        _timer = 0f;
        OrderMB orderMb;
        if (Orders.Count == 0)
        {
            orderMb = Instantiate(_orderMbPrefab, this.transform);
            orderMb.transform.SetSiblingIndex(0);
        }
        else
        {
            orderMb = Instantiate(_orderMbPrefab, _secondaryOrders);
            orderMb.transform.localScale = Vector3.one * 0.5f;
        }

        Orders.Add(orderMb);
        _nextOrderTime = GetNextOrderTime();
        _orderAudioSource.clip = _orderSound;
        _orderAudioSource.Play();
        Debug.Log("AddNewOrderTime: " + _nextOrderTime);
    }

    public bool TryGetMatchingOrder(Order input, out OrderMB matchingOrderMb)
    {
        matchingOrderMb = Orders.FirstOrDefault(o => o.DoOrdersMatch(input));
        if (matchingOrderMb != null)
        {
            Debug.Log("Matching order found");
            return true;
        }

        PopupText.Instance.ShowPopup("No matching order found");
        return false;
    }

    public void RecordFullfilledOrder(decimal earnings)
    {
        OrdersFullfilled++;
        Cash += earnings;
        _cashText.text = $"${Cash}";

        if (OrdersFullfilled >= NEXT_LEVEL_THRESHOLD)
        {
            Level.Instance.NextLevel();
            Reset();
        }
    }

    public void RecordTrashedTea(decimal cost)
    {
        Cash -= cost;
        _cashText.text = $"${Cash}";
    }

    public void ClearOrders()
    {
        foreach (var order in Orders)
        {
            if (order == null) { continue; }
            Debug.Log("Destroying order");
            Destroy(order.gameObject);
        }
    }

    public void CompleteOrder(OrderMB order)
    {
        Orders.Remove(order);
        Destroy(order.gameObject);
        if (Orders.Count > 0)
        {
            var newPrimaryOrder = _secondaryOrders.transform.GetChild(0);
            newPrimaryOrder.parent = this.transform;
            newPrimaryOrder.localScale = Vector3.one;
            newPrimaryOrder.SetSiblingIndex(0);
        }
    }

    public void GameOver(string text = "GAME OVER")
    {
        HighScore.Instance.SetHighScore(Cash);
        Debug.Log("Order failed");
        PopupText.Instance.GameOver(text);
        OrderSystem.Instance.ClearOrders();
        Music.Instance.StopMusic();
        StartCoroutine(RestartGame());
        _isGameOver = true;
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Reset()
    {
        ClearOrders();
        Orders.Clear();
        OrdersFullfilled = 0;
    }
}
