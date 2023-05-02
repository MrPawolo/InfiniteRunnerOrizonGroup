using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ML.PhysicsEventsCaller
{
    public abstract class PhysicsEventsCallerBase : MonoBehaviour
    {
        protected Dictionary<Collider, CallsContainer> calls = new Dictionary<Collider, CallsContainer>();


        public void SubscribeEvent(ColliderCollection collection)
        {
            foreach(Collider collider in collection.colliders)
            {
                if(calls.ContainsKey(collider))
                {
                    List<IPhysicsCalls> physicsCalls = calls[collider].calls;
                    if (physicsCalls.Contains(collection.physicsCall))
                        break;

                    physicsCalls.Add(collection.physicsCall);
                }
                else
                {
                    CallsContainer callsContainer = new CallsContainer();
                    List<IPhysicsCalls> physicsCalls = callsContainer.calls;
                    if (physicsCalls.Contains(collection.physicsCall))
                        break;

                    physicsCalls.Add(collection.physicsCall);
                    calls.Add(collider, callsContainer);
                }
            }
        }

        public void UnsubscribeEvent(ColliderCollection collection)
        {
            List<Collider> toRemoveColliders = new List<Collider>();

            foreach (Collider collider in collection.colliders)
            {
                if(calls.TryGetValue(collider, out CallsContainer call))
                {
                    if (call.calls.Contains(collection.physicsCall))
                        call.calls.Remove(collection.physicsCall);

                    if(call.calls.Count == 0)
                        toRemoveColliders.Add(collider);
                }
                else
                {
                    toRemoveColliders.Add(collider);
                }
            }

            foreach(Collider toRemove in toRemoveColliders)
            {
                calls.Remove(toRemove);
            }
        }

        protected void HandlePhysicsEvent(Collision collision,Action<IPhysicsCalls> onEvent)
        {
            ContactPoint[] points = new ContactPoint[collision.contactCount];
            collision.GetContacts(points);
            foreach (ContactPoint point in points)
            {
                if (calls.TryGetValue(point.thisCollider, out CallsContainer container))
                {
                    foreach (IPhysicsCalls call in container.calls)
                        onEvent(call);
                }
            }
        }
    }

}
