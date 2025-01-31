using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<CanvasGroup>().alpha = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
