using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeScreen : MonoBehaviour
{
    [SerializeField]
    private int fadeTime = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeOut());
    }
    
    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(fadeTime);
        this.gameObject.SetActive(false);
    }
}
