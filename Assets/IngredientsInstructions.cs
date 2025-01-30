using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    public IEnumerator ShowIngredientToKeyInstructions()
    {
        while (true)
        {
            _canvasGroup.alpha = 1f;
            if (Level.Instance.LevelIndex == 1)
            {
                if (_instructionIndex == 0)
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
                else if (_instructionIndex == 2)
                {
                    _image.sprite = IconManager.Instance.MilkSprite;
                    _key.text = "M";
                    _text.text = "for Milk";
                    _listenForKey = KeyCode.M;
                }
                else if (_instructionIndex == 3)
                {
                    _image.sprite = IconManager.Instance.SugarSprite;
                    _key.text = "S";
                    _text.text = "for Sugar";
                    _listenForKey = KeyCode.S;
                }
                else if (_instructionIndex == 4)
                {
                    _image.sprite = IconManager.Instance.RegularTeaSprite;
                    _key.text = "T";
                    _text.text = "for Tea";
                    _listenForKey = KeyCode.T;
                }
                else if (_instructionIndex > 4)
                {
                    DidCompleteTutorial = true;
                    _canvasGroup.alpha = 0f;
                    Reset();
                    break;
                }

                yield return null;
            }
        }
    }

    public void Reset()
    {
        _listenForKey = KeyCode.None;
        _instructionIndex = 0;
    }
}
