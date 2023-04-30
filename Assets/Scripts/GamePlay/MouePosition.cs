using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ML.GameEvents;
using System;

namespace ML.GamePlay
{
    public class MouePosition : MonoBehaviour
    {
        [SerializeField] VoidListener onGameStart;
        [SerializeField] VoidListener onGameOver;

        bool isPlaying = false;

        private void Awake()
        {
            onGameStart.onGameEventInvoke += HandleStartGame;
            onGameOver.onGameEventInvoke += HandleGameOver;
        }

        private void HandleGameOver(GameEvents.Void obj)
        {
            isPlaying = false;
        }

        private void HandleStartGame(GameEvents.Void obj)
        {
            isPlaying = true;
        }

        private void OnEnable()
        {
            onGameStart.HookToGameEvent();
            onGameOver.HookToGameEvent();
        }

        private void OnDisable()
        {

            onGameStart.UnHookFromGameEvent();
            onGameOver.UnHookFromGameEvent();
        }

        public float GetMousePos()
        {
            if (isPlaying)
                return Mouse.current.position.x.value;
            else
                return Screen.width / 2;
        }

        public float GetNormalizedScreenXPos()
        {
            float xMousePosition = GetMousePos();
            float normalizedXMousePosition = xMousePosition / Screen.width;
            normalizedXMousePosition = Mathf.Clamp01(normalizedXMousePosition);
            return normalizedXMousePosition;
        }
    }
}
