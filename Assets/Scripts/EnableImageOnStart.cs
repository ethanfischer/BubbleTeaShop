using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableImageOnStart : MonoBehaviour
{
    [SerializeField]
    Image _image;
    
    // Start is called before the first frame update
    void Start()
    {
        _image.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
