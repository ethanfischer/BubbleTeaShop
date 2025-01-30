using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientsInstructions : MonoBehaviour
{
    [SerializeField]
    Image _image;

    [SerializeField]
    TMP_Text _key;

    [SerializeField]
    TMP_Text _text;
    [SerializeField]
    CanvasGroup _canvasGroup;
    KeyCode _listenForKey;
    int _instructionIndex;


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

    public void ShowIngredientToKeyInstructions()
    {
        _canvasGroup.alpha = 1f; //TODO: why is this not working?
        if (Level.Instance.LevelIndex == 1)
        {
            if(_instructionIndex == 0)
            {
                _image.sprite = IconManager.Instance.RegularBobaSprite;
                _key.text = "B";
                _text.text = "for bubbles";
                _listenForKey = KeyCode.B;
            }
            else if (_instructionIndex == 1)
            {
                _image.sprite = IconManager.Instance.IceSprite;
                _key.text = "I";
                _text.text = "for Ice";
                _listenForKey = KeyCode.I;
            }
        }
    }

    public void Reset()
    {
        _listenForKey = KeyCode.None;
        _instructionIndex = 0;
    }
}
