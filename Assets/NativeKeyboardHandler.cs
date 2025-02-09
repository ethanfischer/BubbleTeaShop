using UnityEngine;

public class NativeKeyboardHandler : MonoBehaviour
{
    TouchScreenKeyboard _keyboard;
    string _typedText = "";
    string _previousText = "";
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

            // Detect new characters
            if (_typedText.Length > _previousText.Length)
            {
                var lastChar = _typedText[_typedText.Length - 1];

                switch (lastChar)
                {
                    case 'J':
                    case 'j':
                        KeyCode = KeyCode.J;
                        break;
                    case 'E':
                    case 'e':
                        KeyCode = KeyCode.E;
                        break;
                    case 'B':
                    case 'b':
                        KeyCode = KeyCode.B;
                        break;
                    case 'C':
                    case 'c':
                        KeyCode = KeyCode.C;
                        break;
                    case 'M':
                    case 'm':
                        KeyCode = KeyCode.M;
                        break;
                    case 'S':
                    case 's':
                        KeyCode = KeyCode.S;
                        break;
                    case 'I':
                    case 'i':
                        KeyCode = KeyCode.I;
                        break;
                    case 'F':
                    case 'f':
                        KeyCode = KeyCode.F;
                        break;
                    case 'H':
                    case 'h':
                        KeyCode = KeyCode.H;
                        break;
                    case 'T':
                    case 't':
                        KeyCode = KeyCode.T;
                        break;
                    case 'X':
                    case 'x':
                        KeyCode = KeyCode.X;
                        break;
                    case 'P':
                    case 'p':
                        KeyCode = KeyCode.P;
                        break;
                    case ' ':
                        KeyCode = KeyCode.Space;
                        break;
                    default:
                        KeyCode = KeyCode.None;
                        break;
                }

                _previousText = _typedText;
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
