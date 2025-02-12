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
        if (Application.isEditor)
        {
            HandleDesktop();
        }
        else
        {
            HandleMobile();
        }

        Debug.Log($"KeyCode: {KeyCode}");
    }

    void HandleDesktop()
    {
        // Check for specific key presses
        if (Input.GetKeyDown(KeyCode.J)) KeyCode = KeyCode.J;
        else if (Input.GetKeyDown(KeyCode.E)) KeyCode = KeyCode.E;
        else if (Input.GetKeyDown(KeyCode.B)) KeyCode = KeyCode.B;
        else if (Input.GetKeyDown(KeyCode.C)) KeyCode = KeyCode.C;
        else if (Input.GetKeyDown(KeyCode.M)) KeyCode = KeyCode.M;
        else if (Input.GetKeyDown(KeyCode.S)) KeyCode = KeyCode.S;
        else if (Input.GetKeyDown(KeyCode.I)) KeyCode = KeyCode.I;
        else if (Input.GetKeyDown(KeyCode.F)) KeyCode = KeyCode.F;
        else if (Input.GetKeyDown(KeyCode.H)) KeyCode = KeyCode.H;
        else if (Input.GetKeyDown(KeyCode.T)) KeyCode = KeyCode.T;
        else if (Input.GetKeyDown(KeyCode.X)) KeyCode = KeyCode.X;
        else if (Input.GetKeyDown(KeyCode.P)) KeyCode = KeyCode.P;
        else if (Input.GetKeyDown(KeyCode.L)) KeyCode = KeyCode.L;
        else if (Input.GetKeyDown(KeyCode.R)) KeyCode = KeyCode.R;
        else if (Input.GetKeyDown(KeyCode.Space)) KeyCode = KeyCode.Space;
        else KeyCode = KeyCode.None; // No relevant key detected
    }

    void HandleMobile()
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
                    'L' or 'l' => KeyCode.L,
                    'R' or 'r' => KeyCode.R,
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
