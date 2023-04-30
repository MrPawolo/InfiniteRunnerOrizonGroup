using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ML.GameEvents;
using System;

namespace ML.GamePlay
{
    public class PointsManager : MonoBehaviour
    {
        [SerializeField] IntEvent onPointsChanged;
        [SerializeField] VoidListener onGameStart;
        [SerializeField] VoidListener onGameOver;
        [SerializeField] float scorePointsTimeInterval = 1;
        public static PointsManager Instance { get; private set; }

        int points = 0;
        bool isPlaying = false;
        float elapsedTime = 0;
        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }

            onGameStart.onGameEventInvoke += HandleGameStart;
            onGameOver.onGameEventInvoke += handleGameOver;
            onGameStart.HookToGameEvent();
            onGameOver.HookToGameEvent();
        }

        private void handleGameOver(GameEvents.Void obj)
        {
            isPlaying = false;
        }

        private void HandleGameStart(GameEvents.Void obj)
        {
            isPlaying = true;
            points = 0;
            elapsedTime = 0;
            onPointsChanged.Invoke(points);
        }

        private void OnDestroy()
        {
            onGameStart.UnHookFromGameEvent();
            onGameOver.UnHookFromGameEvent();
        }

        // Update is called once per frame
        void Update()
        {
            if (!isPlaying)
                return;

            elapsedTime += Time.deltaTime;

            if(elapsedTime >= scorePointsTimeInterval)
            {
                elapsedTime -= scorePointsTimeInterval;
                points = AddPoints(points);
                onPointsChanged.Invoke(points);
            }
        }

        int AddPoints(int currentPoints)
        {
            currentPoints++;
            return currentPoints;
        }
    }
}
