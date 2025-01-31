using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientInstruction : MonoBehaviour
{
    // Animator _animator;

    void Start()
    {
        // _animator = GetComponent<Animator>();
    }
    
    public void FadeIn()
    {
        // _animator.enabled = true;
        // _animator.Play("IngredientInstructionFadeIn");
        GetComponent<CanvasGroup>().alpha = 1f;
    }

    public void OnFadeInComplete()
    {
        // _animator.enabled = false;
    }
    
    public void Hide()
    {
        GetComponent<CanvasGroup>().alpha = 0f;
    }
}
