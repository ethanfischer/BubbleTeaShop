using System;
using System.Linq;
using TMPro;
using UnityEngine;
using Input = NativeKeyboardHandler;

public class TutorialState : MonoBehaviour, IState
{
    TMP_Text _key;
    TMP_Text _text;
    KeyCode _listenForKey = KeyCode.ScrollLock;
    int _instructionIndex;
    static bool _didSetKeyAndText;
    Level _level;
    OrderMB _order;
    ActiveTea _activeTea;
    Action _listenForKeyAction;
    [SerializeField]
    GameObject _activeTeaUI;
    private Level Level
    { get
    {
        if (_level == null)
        {
            _level = Level.Instance;
        }
        return _level;
    } }

    public static bool[] CompletedTutorials { get; private set; } =
    {
        false,//0
        false,//1
        true, //2 doesn't need a tutorial
        false,//3
        false,//4
        false,//5
        false,//6
        false,
        false,
        false,
        false
    };

    public void Enter()
    {
        Debug.Log("Showing tutorial");
        _order = OrderSystem.Instance.Orders.First();
        _activeTea = FindObjectOfType<ActiveTea>();
    }

    void IState.Tick()
    {
        if (Input.GetKeyDown(_listenForKey))
        {
            _listenForKeyAction?.Invoke();
            NextInstruction();
        }

        ShowIngredientToKeyInstructions(Level.LevelIndex);
    }

    private void CompleteTutorial()
    {
        SetPopupKeyAndText(-1, string.Empty, string.Empty, KeyCode.None);
        SetIngredientInstructionKeyAndText(-1, string.Empty, string.Empty, KeyCode.None);
        _order.Complete();
        CompletedTutorials[Level.LevelIndex] = true;
        StateMachineService.Instance.SetDefaultState();
    }

    public void Exit()
    {
        Reset();
    }

    void NextInstruction()
    {
        _instructionIndex++;
        _didSetKeyAndText = false;
        _order.Instruction1.GetComponent<IngredientInstruction>().Hide(); //TODO: just serialize ingredient instruction instead of gameobject and then looking itn up
        _order.Instruction2.GetComponent<IngredientInstruction>().Hide();
        _order.Instruction3.GetComponent<IngredientInstruction>().Hide();
        _order.Instruction4.GetComponent<IngredientInstruction>().Hide();
        _order.Instruction5.GetComponent<IngredientInstruction>().Hide();
        _order.Instruction6.GetComponent<IngredientInstruction>().Hide();
    }

    void ShowIngredientToKeyInstructions(int i)
    {
        switch (i)
        {
            case 1:
                Level1();
                break;
            case 2:
                Level2();
                break;
            case 3:
                Level3();
                break;
            case 4:
                Level4();
                break;
            case 5:
                Level5();
                break;
            case 6:
                Level6();
                break;
            default:
                PopupText.Instance.ShowPopup("You won", float.MaxValue);
                throw new ArgumentOutOfRangeException(nameof(i), i, null);
        }
    }

    void Level1()
    {
        switch (_instructionIndex)
        {
            case 0:
                SetPopupKeyAndText(1, "c", "for Cup", KeyCode.C, () =>
                {
                    _activeTea.AddCup(CupSize.Cup);
                    Debug.Log("Added cup");
                });
                break;
            case 1:
                SetIngredientInstructionKeyAndText(1, "b", "for Boba", KeyCode.B, () =>
                {
                    _activeTea.AddRegularBoba();
                });
                break;
            case 2:
                SetIngredientInstructionKeyAndText(2, "i", "for Ice", KeyCode.I, () => _activeTea.AddIce());
                break;
            case 3:
                SetIngredientInstructionKeyAndText(3, "m", "for Milk", KeyCode.M, () => _activeTea.AddMilk());
                break;
            case 4:
                SetIngredientInstructionKeyAndText(4, "s", "for Sugar", KeyCode.S, () => _activeTea.AddSugar());
                break;
            case 5:
                SetIngredientInstructionKeyAndText(5, "t", "for Tea", KeyCode.T, () =>
                {
                    _activeTea.AddRegularTea();
                });
                break;
            case 6:
                SetPopupKeyAndText(5, "space", "to submit", KeyCode.Space, () => _activeTea.SubmitTeaForTutorial());
                break;
            case 7:
                SetPopupKeyAndText(5, "x", "to trash", KeyCode.X, () => _activeTea.TrashTeaForTutorial());
                break;
            case > 7:
                CompleteTutorial();
                break;
        }
    }

    void Level2()
    {
        CompleteTutorial();
    }

    void Level3()
    {
        switch (_instructionIndex)
        {
            case 0:
                SetIngredientInstructionKeyAndText(1, "j", "for Jelly", KeyCode.J);
                break;
            case 1:
                SetIngredientInstructionKeyAndText(6, "f", "for cheese Foam", KeyCode.F);
                break;
            case > 1:
                CompleteTutorial();
                break;
        }
    }

    void Level4()
    {
        switch (_instructionIndex)
        {
            case 0:
                SetIngredientInstructionKeyAndText(6, "c", "for Cups", KeyCode.C, () =>
                {
                    CameraManager.Instance.ActivateCupPose();
                    CupSelection.Instance.ShowCupSelection(CupSize.LargeCup);
                });
                break;
            case 1:
                SetPopupKeyAndText(1, "c", "for Cup", KeyCode.C);
                break;
            case 2:
                SetPopupKeyAndText(1, "l", "for Large cup", KeyCode.L, () =>
                {
                    CameraManager.Instance.ActivateDefaultPose();
                    CupSelection.Instance.HideCupSelection();
                });
                break;
            case > 2:
                CompleteTutorial();
                break;
        }
    }


    void Level5()
    {
        switch (_instructionIndex)
        {
            case 0:
                SetIngredientInstructionKeyAndText(1, "B", "for Boba Flavors", KeyCode.B, () => CameraManager.Instance.ActivateBobaPose2());
                break;
            case 1:
                SetPopupKeyAndText(6, "B", "Boba", KeyCode.B);
                break;
            case 2:
                SetPopupKeyAndText(6, "L", "b<u>L</u>ueberry", KeyCode.L);
                break;
            case 3:
                SetPopupKeyAndText(6, "M", "Mango", KeyCode.M);
                break;
            case 4:
                SetPopupKeyAndText(6, "S", "Strawberry", KeyCode.S, () => CameraManager.Instance.ActivateDefaultPose());
                break;
            case > 4:
                CompleteTutorial();
                break;
        }
    }

    void Level6()
    {
        switch (_instructionIndex)
        {
            case 0:
                SetIngredientInstructionKeyAndText(5, "T", "for Tea Flavors", KeyCode.T, () => CameraManager.Instance.ActivateTeaPose());
                break;
            case 1:
                SetPopupKeyAndText(6, "M", "Matcha", KeyCode.M);
                break;
            case 2:
                SetPopupKeyAndText(6, "T", "Tea", KeyCode.T);
                break;
            case 3:
                SetPopupKeyAndText(6, "R", "ta<u>R</u>o", KeyCode.R, () => CameraManager.Instance.ActivateDefaultPose());
                break;
            case > 1:
                CompleteTutorial();
                break;
        }
    }

    void SetIngredientInstructionKeyAndText(int index, string key, string text, KeyCode listenForKey, Action onComplete = null)
    {
        if (_didSetKeyAndText) return;

        PopupText.Instance.ShowPopup(string.Empty, 0); //Clear out any previous popups
        _listenForKey = listenForKey;
        _listenForKeyAction = onComplete;
        _didSetKeyAndText = true;

        _order.Instruction1.GetComponent<IngredientInstruction>().Hide(); //TODO: just serialize ingredient instruction instead of gameobject and then looking itn up
        _order.Instruction2.GetComponent<IngredientInstruction>().Hide();
        _order.Instruction3.GetComponent<IngredientInstruction>().Hide();
        _order.Instruction4.GetComponent<IngredientInstruction>().Hide();
        _order.Instruction5.GetComponent<IngredientInstruction>().Hide();
        _order.Instruction6.GetComponent<IngredientInstruction>().Hide();

        GameObject instruction = null;

        if (index == -1) return;

        switch (index)
        {
            case 1:
                instruction = _order.Instruction1;
                break;
            case 2:
                instruction = _order.Instruction2;
                break;
            case 3:
                instruction = _order.Instruction3;
                break;
            case 4:
                instruction = _order.Instruction4;
                break;
            case 5:
                instruction = _order.Instruction5;
                break;
            case 6:
                instruction = _order.Instruction6;
                break;
            default:
                break;
        }

        instruction.transform.Find("Key").transform.GetChild(1).GetComponent<TMP_Text>().text = key;
        instruction.transform.Find("Text").gameObject.GetComponent<TMP_Text>().text = text;

        instruction.GetComponent<IngredientInstruction>().FadeIn(); //TODO: just serialize ingredient instruction instead of gameobject and then looking itn up

    }

    void SetPopupKeyAndText(int index, string key, string text, KeyCode listenForKey, Action onComplete = null)
    {
        if (_didSetKeyAndText) return;

        _listenForKey = listenForKey;
        _listenForKeyAction = onComplete;
        _didSetKeyAndText = true;

        PopupText.Instance.ShowPopup(text, float.MaxValue, key);
    }

    public void Reset()
    {
        _listenForKey = KeyCode.ScrollLock;
        _instructionIndex = 0;
        _didSetKeyAndText = false;
    }
}
