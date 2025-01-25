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

    public void ShowPopup(string text)
    {
        _text.text = text;
        _timer = 2f;
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
