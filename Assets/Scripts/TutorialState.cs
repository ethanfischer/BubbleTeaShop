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
    static bool _didSetIngredientInstructionKeyAndText;
    private Level _level;
    OrderMB _order;
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
    }

    void IState.Update()
    {
        if (Input.GetKeyDown(_listenForKey))
        {
            NextInstruction();
        }

        ShowIngredientToKeyInstructions(Level.LevelIndex);
    }

    private void CompleteTutorial()
    {
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
        _didSetIngredientInstructionKeyAndText = false;
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
            default:
                throw new ArgumentOutOfRangeException(nameof(i), i, null);
        }
    }

    void Level1()
    {
        if (_instructionIndex == 0)
        {
            SetIngredientInstructionKeyAndText(1, "B", "for Boba");
            _listenForKey = KeyCode.B;
        }
        else if (_instructionIndex == 1)
        {
            SetIngredientInstructionKeyAndText(2, "I", "for Ice");
            _listenForKey = KeyCode.I;
        }
        else if (_instructionIndex == 2)
        {
            SetIngredientInstructionKeyAndText(3, "M", "for Milk");
            _listenForKey = KeyCode.M;
        }
        else if (_instructionIndex == 3)
        {
            SetIngredientInstructionKeyAndText(4, "S", "for Sugar");
            _listenForKey = KeyCode.S;
        }
        else if (_instructionIndex == 4)
        {
            SetIngredientInstructionKeyAndText(5, "T", "for Tea");
            _listenForKey = KeyCode.T;
        }
        else if (_instructionIndex == 5)
        {
            SetIngredientInstructionKeyAndText(6, "", "Enter to submit");
            _listenForKey = KeyCode.Return;
        }
        else if (_instructionIndex == 6)
        {
            SetIngredientInstructionKeyAndText(6, "X", "to trash");
            _listenForKey = KeyCode.X;
        }
        else if (_instructionIndex > 6)
        {
            CompleteTutorial();
        }
    }

    void Level2()
    {
        CompleteTutorial();
    }

    void Level3()
    {
        if (_instructionIndex == 0)
        {
            SetIngredientInstructionKeyAndText(6, "C", "for Cheese Foam");
            _listenForKey = KeyCode.C;
        }
        else if (_instructionIndex > 0)
        {
            CompleteTutorial();
        }
    }

    void Level4()
    {
        if (_instructionIndex == 0)
        {
            SetIngredientInstructionKeyAndText(1, "B", "for Boba Flavors");
            _listenForKey = KeyCode.B;
        }
        else if (_instructionIndex == 1)
        {
            SetIngredientInstructionKeyAndText(6, "S", "for Strawberry"); //TODO: show all the flavors in the order
            _listenForKey = KeyCode.S;
        }
        else if (_instructionIndex > 1)
        {
            CompleteTutorial();
        }
    }

    void Level5()
    {
        if (_instructionIndex == 0)
        {
            SetIngredientInstructionKeyAndText(1, "T", "for Tea Flavors");
            _listenForKey = KeyCode.T;
        }
        else if (_instructionIndex == 1)
        {
            SetIngredientInstructionKeyAndText(6, "M", "for Matcha"); //TODO: show all the flavors in the order
            _listenForKey = KeyCode.M;
        }
        else if (_instructionIndex > 1)
        {
            CompleteTutorial();
        }
    }

    void SetIngredientInstructionKeyAndText(int index, string key, string text)
    {
        if (_didSetIngredientInstructionKeyAndText) return;

        _didSetIngredientInstructionKeyAndText = true;

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

    public void Reset()
    {
        _listenForKey = KeyCode.None;
        _instructionIndex = 0;
        _didSetIngredientInstructionKeyAndText = false;
    }
}
