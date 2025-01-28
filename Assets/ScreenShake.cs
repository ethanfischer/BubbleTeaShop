using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    //unity singleton patter
    private static ScreenShake instance;
    public static ScreenShake Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScreenShake>();
            }
            return instance;
        }
    }
    
    private RectTransform uiElement; // The UI element to shake
    private Vector3 originalPosition;
    float _magnitude;
    float _duration;

    void Start()
    {
        uiElement = GetComponent<RectTransform>();
        if (uiElement != null)
        {
            originalPosition = uiElement.anchoredPosition;
        }
    }

    public void TriggerShake(float duration = 0.5f, float magnitude = 10f)
    {
        return; //needs testing
        _duration = duration;
        _magnitude = magnitude;
        if (uiElement != null)
        {
            StopAllCoroutines();
            StartCoroutine(Shake());
        }
    }

    private IEnumerator Shake()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            float offsetX = Random.Range(-1f, 1f) * _magnitude;
            float offsetY = Random.Range(-1f, 1f) * _magnitude;

            uiElement.anchoredPosition = new Vector3(
                originalPosition.x + offsetX,
                originalPosition.y + offsetY,
                originalPosition.z
            );

            elapsedTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        uiElement.anchoredPosition = originalPosition; // Reset position
    }
}
