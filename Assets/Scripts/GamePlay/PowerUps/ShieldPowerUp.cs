using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ML.Extensions;
using ML.PhysicsEventsCaller;

namespace ML.GamePlay
{
    public class ShieldPowerUp : OnCollisionEnterHandler, IPowerUp
    {
        [SerializeField] LayerMask obstacleLayerMask;
        [SerializeField] UnityEvent onDestroy;

        protected override void Awake()
        {
            colliderCollection = new ColliderCollection(collidersToRecieveCall, this);
            PhysicsEventsCallerBase caller = GetComponentInParent<PhysicsEventsCallerBase>();

            if (caller != null)
            {
                base.caller = caller;
                caller.SubscribeEvent(colliderCollection);
            }
        }


        public override void OnCollision(Collision collision)
        {

            GameObject obstacle = collision?.rigidbody?.gameObject;
            if (!obstacle)
                return;

            if (!obstacleLayerMask.ContainsLayer(obstacle.layer))
                return;

            onDestroy?.Invoke();

            if (obstacle.TryGetComponent(out IDestroy destroy))
            {
                destroy.Destroy();
            }
            Destroy(gameObject);
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}
