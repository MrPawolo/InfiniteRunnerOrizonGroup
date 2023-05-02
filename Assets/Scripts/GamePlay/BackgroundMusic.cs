using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] AudioClip[] clips = new AudioClip[0];
    AudioSource music;
    static bool instanced = false;

    private void Awake()
    {
        if (!instanced)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);

        music = GetComponent<AudioSource>();
        music.playOnAwake = false;
    }

    private void Update()
    {
        if (music.isPlaying)
            return;

        music.Play();
        music.clip = clips[Random.Range(0,clips.Length)];
    }
}
