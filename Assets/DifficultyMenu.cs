using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyMenu : MonoBehaviour
{
    [SerializeField]
    Animator _animator;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeOut()
    {
        _animator.enabled = true;
    }
    
    public void OnAnimationFinished()
    {
        Destroy(gameObject);
    }
}
