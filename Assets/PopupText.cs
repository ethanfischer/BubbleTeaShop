using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour
{
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

    public void ShowPopup(string text, float f = 2f)
    {
        if (_isGameOver) return;
        
        _text.text = text;
        _timer = f;
    }
    
    public void GameOver()
    {
        _isGameOver = true;
        _text.text = "YOU MISSED AN ORDER.\nGAME OVER";
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
