using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ML.Extensions;
using System;

namespace ML.GamePlay
{
    public class PowerUpColector : MonoBehaviour, IPoolable
    {
        [SerializeField] LayerMask playerMask;
        [SerializeField] PowerUpContainer powerUpContainer;
        [SerializeField] UnityEvent onCollect;

        public Action<GameObject> onRelease { get; set; }
        public Action onGet { get; set; }

        readonly static float DESPAWN_DISTANCE = 50;
        Transform cameraTransform;

        private void Awake()
        {
            cameraTransform = Camera.main.transform;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!playerMask.ContainsLayer(other.gameObject.layer))
                return;

            if (other.GetComponentInChildren<IPowerUp>() != null)
                return;

            Transform playerTransfomr = other.transform;
            Instantiate(
                powerUpContainer.PowerUpPrefab, 
                playerTransfomr.position, 
                playerTransfomr.rotation, 
                playerTransfomr);

            if (!gameObject.activeSelf)
                return;

            onCollect.Invoke();
            ForceRelease();
        }

        void Update()
        {
            if (CanDespawn())
            {
                ForceRelease();
            }
        }

        private bool CanDespawn()
        {
            return (cameraTransform.position.z - transform.position.z) >= DESPAWN_DISTANCE;
        }

        public void ForceRelease()
        {
            onRelease?.Invoke(gameObject);
        }
    }
}
