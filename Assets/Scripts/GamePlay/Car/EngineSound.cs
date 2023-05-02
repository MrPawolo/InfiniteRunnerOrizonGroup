using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ML.GameEvents;

public class EngineSound : MonoBehaviour
{
    [SerializeField] VoidListener onGameStart;
    [SerializeField] VoidListener onGameOver;
    [SerializeField] AudioSource audioSource;
    [SerializeField,Range(0,1)] float shiftUpRev = 0.7f;
    [SerializeField] Vector2 minMaxPitch;
    [SerializeField] Vector2 minMaxVolume;
    [SerializeField] float revUpSpeedNormalized = 0.1f;

    bool isPlaying = false;

    float rev;

    private void Awake()
    {
        onGameStart.onGameEventInvoke += HandleGameStart;
        onGameOver.onGameEventInvoke += HandleGameOver;
        onGameStart.HookToGameEvent();
        onGameOver.HookToGameEvent();
    }


    private void OnDisable()
    {
        onGameStart.UnHookFromGameEvent();
        onGameOver.UnHookFromGameEvent();
    }
    void Update()
    {
        if (!isPlaying)
            return;

        rev += revUpSpeedNormalized * Time.deltaTime;

        if (rev >= 1)
        {
            rev = shiftUpRev;
        }

        audioSource.volume = Mathf.Lerp(minMaxVolume.x, minMaxVolume.y, rev);
        audioSource.pitch = Mathf.Lerp(minMaxPitch.x, minMaxPitch.y, rev);
    }

    private void HandleGameOver(Void obj)
    {
        audioSource.Stop();
        isPlaying = false;
    }

    private void HandleGameStart(Void obj)
    {
        audioSource.Play();
        rev = 0;
        isPlaying = true;
    }
}
