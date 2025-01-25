using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Order : MonoBehaviour
{
    public TMP_Text SizeText;
    public TMP_Text ToppingsText;
    public TMP_Text SugarText;
    public TMP_Text IceText;

    string _size;
    string _toppings;
    string _sugar;
    string _ice;
    
    public void Initialize(string size, string toppings, string sugar, string ice)
    {
        _size = size;
        _toppings = toppings;
        _sugar = sugar;
        _ice = ice;

        SizeText.text = $"Size: {_size}";
        ToppingsText.text = $"Toppings: {_toppings}";
        SugarText.text = $"Sugar: {_sugar}";
        IceText.text = $"Ice: {_ice}";
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
