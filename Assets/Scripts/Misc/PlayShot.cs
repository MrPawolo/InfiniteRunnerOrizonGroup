using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayShot : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    [SerializeField, Range(0, 1)] public float volume = 1; 
    public void Play()
    {
        AudioSource.PlayClipAtPoint(clip, transform.position, volume);
    }
}
