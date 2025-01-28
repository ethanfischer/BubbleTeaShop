using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Hide());
    }
    
    IEnumerator Hide()
    {
        yield return null;  //one frame
        this.gameObject.SetActive(false);
    }
}
