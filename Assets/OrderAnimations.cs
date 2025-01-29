using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderAnimations : MonoBehaviour
{
    public event System.Action OnOrderAnimationCompleteEvent;
    public event System.Action OnOrderArrivedAnimationCompleteEvent;
    public void OnOrderAnimationComplete()
    {
        Debug.Log("Order complete");
        OnOrderAnimationCompleteEvent?.Invoke();
    }
    
    public void OnOrderAnimationArrivedComplete()
    {
        OnOrderArrivedAnimationCompleteEvent?.Invoke();
    }
}
