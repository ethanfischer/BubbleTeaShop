using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeOut());
    }
    
    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }
}
