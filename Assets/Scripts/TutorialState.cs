using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class TutorialState : MonoBehaviour, IState
{
    TMP_Text _key;
    TMP_Text _text;
    KeyCode _listenForKey;
    int _instructionIndex;
    static bool _didSetKeyAndText;
    private Level _level;
    OrderMB _order;
    ActiveTea _activeTea;
    Action _listenForKeyAction;
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
        false,
        false,
        true, //Level 2 doesn't need a tutorial
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
                throw new ArgumentOutOfRangeException(nameof(i), i, null);
        }
    }

    void Level1()
    {
        switch (_instructionIndex)
        {
            case 0:
                SetPopupKeyAndText(1, "C", "view cups", KeyCode.C, () =>
                {
                    CameraManager.Instance.ActivateCupPose();
                    CupSelection.Instance.ShowCupSelection(CupSize.Cup);
                });
                break;
            case 1:
                SetPopupKeyAndText(1, "C", "add cup", KeyCode.C, () =>
                {
                    CameraManager.Instance.ActivateDefaultPose();
                    CupSelection.Instance.HideCupSelection();
                    _activeTea.AddCup(CupSize.Cup);
                });
                break;
            case 2:
                SetIngredientInstructionKeyAndText(1, "B", "Boba", KeyCode.B, () =>
                {
                    CameraManager.Instance.ActivateBobaPose();
                });
                break;
            case 3:
                SetPopupKeyAndText(1, "B", "add boba", KeyCode.B, () =>
                {
                    _activeTea.AddRegularBoba();
                    CameraManager.Instance.ActivateDefaultPose();
                });
                break;
            case 4:
                SetIngredientInstructionKeyAndText(2, "I", "Ice", KeyCode.I, () => _activeTea.AddIce());
                break;
            case 5:
                SetIngredientInstructionKeyAndText(3, "M", "Milk", KeyCode.M, () => _activeTea.AddMilk());
                break;
            case 6:
                SetIngredientInstructionKeyAndText(4, "S", "Sugar", KeyCode.S, () => _activeTea.AddSugar());
                break;
            case 7:
                SetIngredientInstructionKeyAndText(5, "T", "Tea", KeyCode.T, () =>
                {
                    CameraManager.Instance.ActivateTeaPose();
                });
                break;
            case 8:
                SetPopupKeyAndText(1, "T", "add tea", KeyCode.T, () =>
                {
                    _activeTea.AddRegularTea();
                    CameraManager.Instance.ActivateDefaultPose();
                });
                break;
            case 9:
                SetPopupKeyAndText(6, "", "<color=green>Enter</color> to submit", KeyCode.Return, () => _activeTea.SubmitTeaForTutorial());
                break;
            case 10:
                SetPopupKeyAndText(6, "<color=red>X</color>", "to trash", KeyCode.X, () => _activeTea.TrashTeaForTutorial());
                break;
            case > 10:
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
                SetIngredientInstructionKeyAndText(1, "J", "for Jelly", KeyCode.J);
                break;
            case 1:
                SetIngredientInstructionKeyAndText(6, "F", "for Cheese Foam", KeyCode.F);
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
                SetIngredientInstructionKeyAndText(6, "C", "for cups", KeyCode.C, () =>
                {
                    CameraManager.Instance.ActivateCupPose();
                    CupSelection.Instance.ShowCupSelection(CupSize.LargeCup);
                });
                break;
            case 1:
                SetPopupKeyAndText(1, "C", "for cup", KeyCode.C);
                break;
            case 2:
                SetPopupKeyAndText(1, "L", "for large cup", KeyCode.L, () =>
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
                SetIngredientInstructionKeyAndText(1, "B", "for Boba Flavors", KeyCode.B, () => CameraManager.Instance.ActivateBobaPose());
                break;
            case 1:
                SetPopupKeyAndText(6, "S", "Strawberry", KeyCode.S);
                break;
            case 2:
                SetPopupKeyAndText(6, "M", "Mango", KeyCode.M);
                break;
            case 3:
                SetPopupKeyAndText(6, "L", "b<u>L</u>ueberry", KeyCode.L);
                break;
            case 4:
                SetPopupKeyAndText(6, "B", "Boba", KeyCode.B, () => CameraManager.Instance.ActivateDefaultPose());
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
                SetPopupKeyAndText(6, "M", "matcha", KeyCode.M);
                break;
            case 2:
                SetPopupKeyAndText(6, "R", "taRo", KeyCode.R);
                break;
            case 3:
                SetPopupKeyAndText(6, "T", "Tea", KeyCode.T, () => CameraManager.Instance.ActivateDefaultPose());
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

        instruction.transform.Find("Key").gameObject.GetComponent<TMP_Text>().text = key;
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
        _listenForKey = KeyCode.None;
        _instructionIndex = 0;
        _didSetKeyAndText = false;
    }
}
