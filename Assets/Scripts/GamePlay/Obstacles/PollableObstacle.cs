using System;
using System.Collections;
using UnityEngine;

namespace ML.GamePlay
{
    public class PollableObstacle : MonoBehaviour, IPoolable
    {
        public Action<GameObject> forceRelease { get; set; }
        public Action onGet { get; set; }

        readonly static float DESPAWN_DISTANCE = 50;

        Transform cameraTransform;

        private void Awake()
        {
            cameraTransform = Camera.main.transform;
        }

        void Update()
        {
            if (CanDespawn())
            {
                forceRelease?.Invoke(gameObject);
            }
        }

        private bool CanDespawn()
        {
            return (cameraTransform.position.z - transform.position.z) >= DESPAWN_DISTANCE;
        }
    }
}