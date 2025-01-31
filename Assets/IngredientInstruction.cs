using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientInstruction : MonoBehaviour
{
    Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = true;
    }

    public void OnFadeInComplete()
    {
        _animator.enabled = false;
    }
}
