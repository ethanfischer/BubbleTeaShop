using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField]
    AudioClip _recordScratch;
    
    //singleton unity pattern
    private static Music _instance;
    public static Music Instance
    { get
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<Music>();
        }
        return _instance;
    } }
    
    [SerializeField]
    AudioSource _audioSource;
    
    public void StopMusic()
    {
        _audioSource.clip = _recordScratch;
        _audioSource.loop = false;
        _audioSource.Play();
    }
    
    public void PauseMusic()
    {
        _audioSource.Pause();
    }
    
    public void PlayMusic()
    {
        _audioSource.Play();
    }
}
