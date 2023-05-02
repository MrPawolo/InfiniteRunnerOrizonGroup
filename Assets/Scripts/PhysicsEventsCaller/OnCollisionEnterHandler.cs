using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ML.PhysicsEventsCaller
{
    public class OnCollisionEnterHandler : MonoBehaviour, IPhysicsCalls
    {
        [SerializeField] protected PhysicsEventsCallerBase caller;
        [SerializeField] protected Collider[] collidersToRecieveCall;


        protected ColliderCollection colliderCollection;
        protected virtual void Awake()
        {
            colliderCollection = new ColliderCollection(collidersToRecieveCall, this);
            caller.SubscribeEvent(colliderCollection);
        }

        protected virtual void OnDestroy()
        {
            caller.UnsubscribeEvent(colliderCollection);
        }
        public virtual void OnCollision(Collision collision)
        {
            
        }

        public void OnTrigger(Collider collider) {}
    }
}
