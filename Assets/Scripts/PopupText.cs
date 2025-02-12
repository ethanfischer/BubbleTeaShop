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
    
    [SerializeField]
    Animator _animator;
    
    [SerializeField]
    CanvasGroup _canvasGroup;

    [FormerlySerializedAs("_key")]
    [SerializeField]
    TextMeshProUGUI _keyText;
    [SerializeField]
    Transform _key;
    [SerializeField]
    RectTransform _keyPanel;
    
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
        
        // _animator.enabled = true;
        // _animator.Play("PopupFadeIn");
        
        _canvasGroup.alpha = 1f;
        
        _text.text = text;
        _timer = f;

        _keyText.text = key != ""
            ? key
            : string.Empty;
        _key.gameObject.SetActive(key != "");

        _keyPanel.localScale = key == "space"
            ? new Vector3(4f, 1f, 1f)
            : Vector3.one;
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
        _canvasGroup.alpha = 0f;
    }
    
    void Update()
    {
       _timer -= Time.deltaTime;
       if (_timer <= 0f)
       {
           ClearText();
       }
    }
    
    public void OnAnimationEnd()
    {
        _animator.StopPlayback();
        _animator.enabled = false;
    }
}
