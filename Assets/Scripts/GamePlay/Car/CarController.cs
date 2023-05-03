using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ML.InterfaceField;
using ML.GameEvents;
using ML.RootShift;
using ML.Extensions;
using System;

namespace ML.GamePlay
{
    public class CarController : MonoBehaviour, IPositionChanged
    {
        [SerializeField] VoidListener onGameStart;
        [SerializeField] VoidListener onPlayAgain;
        [SerializeField] VoidEvent onCarDied;
        [SerializeField] MouePosition mousePositon;
        [SerializeField] CarNormalizedScreenPos carPos;
        [SerializeField] CarRotate carRotate;
        [SerializeField] CarVelocity carVelocity;

        [SerializeField] float smoothTime = 0.1f;
        float currentMouseVelocity;

        [SerializeField] Rigidbody rb;

        [SerializeField] float vel = 20;

        bool collided = false;

        public float targetHeight;

        Action onPositionChanged;
        Vector3 startPosition;
        Quaternion startRotation;

        public Action OnPositionChanged { get => onPositionChanged; set => onPositionChanged = value; }

        private void Awake()
        {
            startPosition = transform.position.ShiftedToReal();
            startRotation = transform.rotation;

            onGameStart.onGameEventInvoke += HandleGameStart;
            onPlayAgain.onGameEventInvoke += HandlePlayAgain;
        }

        private void OnEnable()
        {
            onGameStart.HookToGameEvent();
            onPlayAgain.HookToGameEvent();
        }

        private void OnDisable()
        {
            onGameStart.UnHookFromGameEvent();
            onPlayAgain.UnHookFromGameEvent();
        }

        private void OnDestroy()
        {
            onGameStart.onGameEventInvoke -= HandleGameStart;
            onPlayAgain.onGameEventInvoke -= HandlePlayAgain;
        }

        private void HandlePlayAgain(GameEvents.Void obj)
        {
            carVelocity.ResetValues();
            rb.useGravity = false;
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            collided = false;
            transform.rotation = startRotation;
            transform.position = startPosition.RealToShifted();
            onPositionChanged?.Invoke();
        }

        private void HandleGameStart(GameEvents.Void obj)
        {
            carVelocity.Vel = vel;
            currentMouseVelocity = 0;
        }

        private void Start()
        {
        }
        private void FixedUpdate()
        {
            if (collided)
                return;

            float newPosition = Mathf.SmoothDamp(
                 carPos.GetNormalizedScreenXPos(),
                 mousePositon.GetNormalizedScreenXPos(),
                 ref currentMouseVelocity,
                 smoothTime);
            carPos.SetNormalizedScreenXPos(newPosition);
            carRotate.RotateCar();
            carVelocity.VelocitiesOnCar();
            rb.angularVelocity = Vector3.zero;
        }


        public void Collision(Collision collision)
        {
            if (collided)
                return;

            if(collision.rigidbody.TryGetComponent(out IDestroy destroy))
            {
                destroy.Destroy();
            }

            collided = true;
            rb.useGravity = true;
            carVelocity.Vel = 0;
            onCarDied.Invoke();
        }

        

        
    }
}
