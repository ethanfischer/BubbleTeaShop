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

    List<OrderMB> _orders = new List<OrderMB>();
    public int OrdersFullfilled { get; private set; }
    float _timer;
    bool _skipFirstFrame = true;

    float _nextOrderTime;
    bool _isGameOver;

    float GetNextOrderTime()
    {
        var upperLimit = GameDifficulty.Difficulty switch
        {
            (int)GameDifficultyEnum.Easy => 30f,
            (int)GameDifficultyEnum.Medium => 15f,
            (int)GameDifficultyEnum.Hard => 10f,
            (int)GameDifficultyEnum.Testing => 1f,
            _ => 0f
        };

        upperLimit -= OrdersFullfilled;
        if (upperLimit <= 1)
        {
            upperLimit = 1;
        }

        var result = Random.Range(0f, upperLimit);

        return result;
    }

    void Start()
    {
        _nextOrderTime = 4.5f;
        _timer = 0f;
    }

    void Update()
    {
        if(_isGameOver) return;
        if (!Tutorial.Instance.DidCloseTutorial) return;

        if (_timer > _nextOrderTime)
        {
            HandleNextOrder();
        }

        _timer += Time.deltaTime;
    }

    void HandleNextOrder()
    {
        _timer = 0f;
        _orders.Add(Instantiate(_orderMbPrefab, this.transform));
        _nextOrderTime = GetNextOrderTime();
        _orderAudioSource.clip = _orderSound;
        _orderAudioSource.Play();
        Debug.Log("AddNewOrderTime: " + _nextOrderTime);
    }

    public bool TryGetMatchingOrder(Order input, out OrderMB matchingOrderMb)
    {
        matchingOrderMb = _orders.FirstOrDefault(o => o.DoOrdersMatch(input));
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

        if (OrdersFullfilled >= 10)
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
        foreach (var order in _orders)
        {
            if (order == null) { continue; }
            Debug.Log("Destroying order");
            Destroy(order.gameObject);
        }
    }

    public void RemoveOrderFromList(OrderMB order)
    {
        _orders.Remove(order);
    }

    public void GameOver(string text = "GAME OVER")
    {
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
        _orders.Clear();
        OrdersFullfilled = 0;
    }
}
