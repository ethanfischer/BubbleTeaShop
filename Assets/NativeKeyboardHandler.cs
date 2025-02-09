using UnityEngine;

public class NativeKeyboardHandler : MonoBehaviour
{
    TouchScreenKeyboard _keyboard;
    string _typedText = "";
    string _previousText = "";
    private float _debounceTime = 0.1f; // Time delay between detections
    private float _nextDetectionTime = 0f; // Tracks when the next key can be detected
    public static KeyCode KeyCode { get; private set; }

    void Start()
    {
        // Show the native keyboard when the game starts
        OpenKeyboard();
    }

    void Update()
    {
        if (_keyboard != null)
        {
            if (_keyboard.status != TouchScreenKeyboard.Status.Visible)
            {
                OpenKeyboard();
                return;
            }

            _typedText = _keyboard.text;

            if (_previousText == _typedText)
            {
                KeyCode = KeyCode.None;
                return;
            }

            // Detect new characters only when debounce period has passed
            if (Time.time >= _nextDetectionTime && _typedText.Length > _previousText.Length)
            {
                var lastChar = _typedText[_typedText.Length - 1];

                KeyCode = lastChar switch
                {
                    'J' or 'j' => KeyCode.J,
                    'E' or 'e' => KeyCode.E,
                    'B' or 'b' => KeyCode.B,
                    'C' or 'c' => KeyCode.C,
                    'M' or 'm' => KeyCode.M,
                    'S' or 's' => KeyCode.S,
                    'I' or 'i' => KeyCode.I,
                    'F' or 'f' => KeyCode.F,
                    'H' or 'h' => KeyCode.H,
                    'T' or 't' => KeyCode.T,
                    'X' or 'x' => KeyCode.X,
                    'P' or 'p' => KeyCode.P,
                    ' ' => KeyCode.Space,
                    _ => KeyCode.None
                };

                // Update the previous text and set the debounce time
                _previousText = _typedText;
                _nextDetectionTime = Time.time + _debounceTime; // Prevent re-detection for debounce duration
            }
        }
    }

    public void OpenKeyboard()
    {
        _keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false);
        TouchScreenKeyboard.hideInput = true;
        _previousText = "";
    }

    public static bool GetKeyDown(KeyCode keyCode) => KeyCode == keyCode;
}
