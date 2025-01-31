using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class IngredientsInstructions : MonoBehaviour
{
    [SerializeField]
    TMP_Text _key;

    [SerializeField]
    TMP_Text _text;
    [SerializeField]
    CanvasGroup _canvasGroup;
    KeyCode _listenForKey;
    int _instructionIndex;

    public bool DidCompleteTutorial { get; private set; } = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_listenForKey))
        {
            NextInstruction();
        }
    }

    void NextInstruction()
    {
        _instructionIndex++;
    }

    public void ShowIngredientToKeyInstructions(OrderMB order)
    {
        if (Level.Instance.LevelIndex == 1)
        {
            if (_instructionIndex == 0)
            {
                SetIngredientInstructionKeyAndText(order, 1, "B", "for Boba");
                _listenForKey = KeyCode.B;
            }
            else if (_instructionIndex == 1)
            {
                SetIngredientInstructionKeyAndText(order, 2, "I", "for Ice");
                _listenForKey = KeyCode.I;
            }
            else if (_instructionIndex == 2)
            {
                SetIngredientInstructionKeyAndText(order, 3, "M", "for Milk");
                _listenForKey = KeyCode.M;
            }
            else if (_instructionIndex == 3)
            {
                SetIngredientInstructionKeyAndText(order, 4, "S", "for Sugar");
                _listenForKey = KeyCode.S;
            }
            else if (_instructionIndex == 4)
            {
                SetIngredientInstructionKeyAndText(order, 5, "T", "for Tea");
                _listenForKey = KeyCode.T;
            }
            else if (_instructionIndex == 5)
            {
                SetIngredientInstructionKeyAndText(order, 6, "", "Enter");
                _listenForKey = KeyCode.Return;
            }
            else if (_instructionIndex > 5)
            {
                DidCompleteTutorial = true;
                _canvasGroup.alpha = 0f;
                Reset();
            }
        }
    }

    static void SetIngredientInstructionKeyAndText(OrderMB order, int index, string key, string text)
    {
        order.Instruction1.GetComponent<IngredientInstruction>().Hide();//TODO: just serialize ingredient instruction instead of gameobject and then looking itn up
        order.Instruction2.GetComponent<IngredientInstruction>().Hide();
        order.Instruction3.GetComponent<IngredientInstruction>().Hide();
        order.Instruction4.GetComponent<IngredientInstruction>().Hide();
        order.Instruction5.GetComponent<IngredientInstruction>().Hide();
        order.Instruction6.GetComponent<IngredientInstruction>().Hide();

        GameObject instruction = null;

        switch (index)
        {
            case 1:
                instruction = order.Instruction1;
                break;
            case 2:
                instruction = order.Instruction2;
                break;
            case 3:
                instruction = order.Instruction3;
                break;
            case 4:
                instruction = order.Instruction4;
                break;
            case 5:
                instruction = order.Instruction5;
                break;
            case 6:
                instruction = order.Instruction6;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
        }

        instruction.GetComponent<IngredientInstruction>().FadeIn(); //TODO: just serialize ingredient instruction instead of gameobject and then looking itn up

        instruction.transform.Find("Key").gameObject.GetComponent<TMP_Text>().text = key;
        instruction.transform.Find("Text").gameObject.GetComponent<TMP_Text>().text = text;
    }

    public void Reset()
    {
        _listenForKey = KeyCode.None;
        _instructionIndex = 0;
    }
}
