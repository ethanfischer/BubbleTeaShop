using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderAnimations : MonoBehaviour
{
    public event System.Action OnOrderAnimationCompleteEvent;
    public void OnOrderAnimationComplete()
    {
        Debug.Log("Order complete");
        OnOrderAnimationCompleteEvent?.Invoke();
    }
}
