using UnityEngine;

public class ActiveIngredientText : MonoBehaviour
{
    [SerializeField]
    Animator _animator;
    // Start is called before the first frame update
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
