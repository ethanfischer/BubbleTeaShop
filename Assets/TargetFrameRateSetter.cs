using UnityEngine;

public class TargetFrameRateSetter : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
    }
}
