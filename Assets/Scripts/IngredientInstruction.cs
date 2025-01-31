using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientInstruction : MonoBehaviour
{
    Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
        _animator.StopPlayback();
    }
    
    public void FadeIn()
    {
        _animator.enabled = true;
        _animator.Play("IngredientInstructionFadeIn");
    }

    public void OnFadeInComplete()
    {
        _animator.StopPlayback();
        _animator.enabled = false;
    }
    
    public void Hide()
    {
        GetComponent<CanvasGroup>().alpha = 0f;
    }
}
