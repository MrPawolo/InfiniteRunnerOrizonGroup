using System;
using System.Collections;
using UnityEngine;

namespace ML.GamePlay
{
    public class PoolableWorldPart : MonoBehaviour, IPoolable
    {
        public Action<GameObject> onRelease { get; set; }
        readonly static float DESPAWN_DISTANCE = 50;

        Transform cameraTransform;

        private void Awake()
        {
            cameraTransform = Camera.main.transform;
        }

        void Update()
        {
            if (CanDespawn())
                onRelease?.Invoke(gameObject);
        }

        private bool CanDespawn()
        {
            return (cameraTransform.position.z - transform.position.z) >= DESPAWN_DISTANCE;
        }
    }
}