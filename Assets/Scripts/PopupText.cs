using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PopupText : MonoBehaviour
{
    [FormerlySerializedAs("_green")]
    [SerializeField]
    Color _earnedCashColor;
    float _timer = 0f;
    //singleton unity pattern
    private static PopupText _instance;
    public static PopupText Instance
    { get
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<PopupText>();
        }
        return _instance;
    } }

    [SerializeField]
    TMP_Text _text;
    bool _isGameOver;

    public void ShowPopup(string text, float f = 2f, string key = "")
    {
        if (_isGameOver) return;
        
        _text.text = text;
        _timer = f;

        transform.Find("Key").GetComponent<TextMeshProUGUI>().text = key != ""
            ? key
            : string.Empty;
    }

    public string GetCashColorHexCode()
    {
        return _earnedCashColor.ToHexString();
    }
    
    public void GameOver(string text = "GAME OVER")
    {
        _isGameOver = true;
        _text.text = text;
        _timer = float.MaxValue;
    }
    
    public void ClearText()
    {
        _text.text = "";
    }
    
    void Update()
    {
       _timer -= Time.deltaTime;
       if (_timer <= 0f)
       {
           ClearText();
       }
    }
}
